GO
ALTER procedure [dbo].[PT_LoadWeight] (        
   @WeightCode varchar(20),      
   @TrafficCode varchar(20),      
   @NavicertCode varchar(20),        
   @MarkedCardCode varchar(20),      
   @RemoteCardCode varchar(20),      
   @CollCode varchar(10),      
   @CollName nvarchar(50),      
   @CoalKindCode varchar(10),      
   @CoalKindName nvarchar(20),      
   @CarOwnerName nvarchar(20),      
   @CarNo nvarchar(20),        
   @CarType nvarchar(20),      
   @LoadWeight decimal(12,2),      
   @EmptyWeight decimal(12,2),       
   @NetWeight decimal(12,2),      
   @OverWeight decimal(12,2),       
   @RoomCode varchar(10),      
   @RoomName nvarchar(50),      
   @BangType nvarchar(20),      
   @Operator nvarchar(20),      
   @WeightTime datetime,      
   @RandomCode  varchar(4),        
   @CustomerName nvarchar(20),      
   @TaxType nvarchar(20),      
   @TaxObject varchar,      
   @IsFirstSite varchar(1),      
   @FrontImage  varchar(32),      
   @BackImage  varchar(32),      
   @UpImage  varchar(32),      
   @RoomImage  varchar(32),      
   @FrontImageContent image,      
   @BackImageContent image,      
   @UpImageContent image,      
   @RoomImageContent image,      
   @EmptyCode varchar(20)      
  ) as
SET XACT_ABORT ON    
begin tran trans       
       
--计算规费      
-----------------------------------------------------------------       
 Declare @TaxMoney decimal(14,4)        
 Declare @FundMoney decimal(14,4)        
 Declare @TaxGroup  numeric(10,0)      
 --判断扣规费的对象 1：磅房 2：煤矿 3：片区    
 Declare @TaxObjectName varchar(20)      
 if(@TaxObject='1')      
  Set @TaxObjectName=@RoomCode      
 else if(@TaxObject='2')     
  Set @TaxObjectName=@CollCode   
 else if(@TaxObject='3')
	Select  @TaxObjectName=ParcelCode from Sys_Colliery where CollCode=@CollCode
         
 Select @TaxMoney = 0.00        
 Select @FundMoney = 0.00       
 Set @TaxGroup=0      
       
 --TaxGroup      
 select @TaxGroup=isnull(max(TaxGroup),0) from TT_TaxItemDetail where EffectTime<=@WeightTime and 

RoomCode=@TaxObjectName and CoalKindCode=@CoalKindCode      
      
 --TaxMoney      
 select @TaxMoney=isnull(sum(Amount),0)  from TT_TaxItemDetail      
 where TaxGroup= @TaxGroup      
 and RoomCode=@TaxObjectName and CoalKindCode=@CoalKindCode      
      
 --FundMoney      
 select @FundMoney=isnull(sum(Amount),0)  from TT_TaxItemDetail      
 where TaxGroup= @TaxGroup     
 and RoomCode=@TaxObjectName and CoalKindCode=@CoalKindCode      
 and TaxItemCode in       
 (      
  select TaxItemCode from TT_TaxItem        
  where ItemType in(select distinct businID from SYS_Dictionary where businName like '%价格调节基金%')      
 )      
-----------------------------------------------------------------       
      
 Declare @EndMoney decimal(14,4)      
 Declare @TradeCode varchar(20)      
  --要扣除的金额      
 Declare @Tax decimal(14,4)       
  --要扣的价格调节基金      
 Declare @Fund decimal(14,4)      
        
 if(@IsFirstSite<>'1') --计算超重时要扣的税和基金      
  begin      
   Set @Tax=@TaxMoney*@OverWeight      
   Set @Fund=@FundMoney*@OverWeight      
  end      
 else --正常扣税时要扣的税和基金      
  begin      
   Set @Tax=@TaxMoney*@NetWeight      
   Set @Fund=@FundMoney*@NetWeight      
  end    

 --计算 余额-扣除税费 是否大于余额下限
 Declare @IsAcountLacking int
 Set @IsAcountLacking=0
 select @IsAcountLacking=count(0) from TT_ColieryAccount where (isnull(Account,0)-@Tax)>=isnull(LowAccount,0) and CollCode=@CollCode
 if(@IsAcountLacking=1)
	begin
	 --插入重车过磅记录       
	 Insert Into TT_LoadWeight
	(WeightCode,TrafficCode,NavicertCode,MarkedCardCode,RemoteCardCode,CollCode,CollName,CoalKindCode,CoalKindName,CarOwnerName,CarNo,CarType,LoadWeight,EmptyWeight,NetWeight,OverWeight,TaxAmount,FundAmount,RoomCode,RoomName,BangType,Operator,WeightTime,RandomCode,CustomerName,TaxType,IsFirstSite,FrontImage,BackImage,UpImage,RoomImage,TaxGroup,EmptyCode)        
	 Values
	(@WeightCode,@TrafficCode,@NavicertCode,@MarkedCardCode,@RemoteCardCode,@CollCode,@CollName,@CoalKindCode,@CoalKindName,@CarOwnerName,@CarNo,@CarType,@LoadWeight,@EmptyWeight,@NetWeight,@OverWeight,@Tax,@Fund,@RoomCode,@RoomName,@BangType,@Operator,@WeightTime,@RandomCode,@CustomerName,@TaxType,@IsFirstSite,@FrontImage,@BackImage,@UpImage,@RoomImage,@TaxGroup,@EmptyCode)       
	     
	 ---------------增加流水帐户记录-------------------------------------------- 
	 Declare @JournalCode int      
	 Select @TradeCode=IsNull(Max(TradeCode)+1,1000000001) From TT_WaterBook      
	 Select @JournalCode=IsNull(Max(JournalCode)+1,1) From TT_WaterBook Where CollCode=@CollCode And JournalCode Is Not Null      
	 Select @EndMoney = IsNull(Account,0.00) From TT_ColieryAccount Where CollCode=@CollCode   
	     
	 Insert Into TT_WaterBook
	(TradeCode,CollCode,JournalCode,StartMoney,TradeMoney,EndMoney,Operator,TradeDate,TradeKind,WeightCode)      
	  Values(@TradeCode,@CollCode,@JournalCode,@EndMoney,-@Tax,@EndMoney -@Tax,@Operator,@WeightTime,@TaxType,@WeightCode)      
	 ---------------------------------------------------------------------------      
	 --煤矿账户减少      
	 Update TT_ColieryAccount Set Account=Account-@Tax where CollCode=@CollCode      
	 --标识卡状态变为已使用      
	 Update TT_MarkedCard set MarkedCardState='2' where MarkedCardCode=@MarkedCardCode and MarkedCardState='1'      
	      
	 declare @count int 
	 --插入车前图片      
	 IF(@FrontImageContent is not null and convert(VARBINARY,@FrontImageContent) <> '')      
	 Begin   
	  select @count=count(*) from SYS_FileSave where FileCode = @FrontImage
	  if(@count = 0)
	   begin
		Insert Into SYS_FileSave(FileCode,FileType,[FileName],FileContent)      
		Values(@FrontImage,'jpg','车前图片',@FrontImageContent) 
	   end
	  set @count = 0     
	 end      
	 --插入车后图片      
	 IF(@BackImageContent is not null and convert(VARBINARY,@BackImageContent) <> '')      
	 begin      
	  select @count=count(*) from SYS_FileSave where FileCode = @BackImage
	  if(@count = 0)
	   begin
		Insert Into SYS_FileSave(FileCode,FileType,[FileName],FileContent)      
		Values(@BackImage,'jpg','车后图片',@BackImageContent)
	   end
	  set @count = 0      
	 end      
	 --插入车厢图片      
	 IF(@UpImageContent is not null and convert(VARBINARY,@UpImageContent) <> '')      
	 begin 
	  select @count=count(*) from SYS_FileSave where FileCode = @UpImage
	  if(@count = 0)
	   begin     
		Insert Into SYS_FileSave(FileCode,FileType,[FileName],FileContent)      
		Values(@UpImage,'jpg','车厢图片',@UpImageContent) 
	   end
	  set @count = 0     
	 end      
	 --插入室内图片      
	 IF(@RoomImageContent is not null and convert(VARBINARY,@RoomImageContent) <> '')      
	 begin
	  select @count=count(*) from SYS_FileSave where FileCode = @RoomImage
	  if(@count = 0)
	   begin       
		Insert Into SYS_FileSave(FileCode,FileType,[FileName],FileContent)      
		Values(@RoomImage,'jpg','室内图片',@RoomImageContent)
	   end
	  set @count = 0      
	 end      
	      
	 --更新空车过磅状态      
	 Update TT_EmptyWeight set IsLoadWeight='1' where NavicertCode=@NavicertCode      
	    
	 Select @WeightCode As WeightCode     
 end
 else
	Select -1 As WeightCode   
   
 IF(@@error=0)    
 Commit tran trans
GO



ALTER procedure [dbo].[PT_LoadWeightUpdate] (  
    @WeightCode varchar(20),   
    @NavicertCode varchar(20),    
    @MarkedCardCode varchar(20),  
    @RemoteCardCode varchar(20),  
    @CollCode varchar(10),  
    @CollName nvarchar(50),  
    @CoalKindCode varchar(10),  
    @CoalKindName nvarchar(20),  
    @CarOwnerName nvarchar(20),  
    @CarNo nvarchar(20),  
    @CarType nvarchar(20),  
    @LoadWeight decimal(9,2),  
    @EmptyWeight decimal(9,2),  
    @NetWeight decimal(9,2),  
    @OverWeight decimal(9,2),  
    @RoomCode varchar(10),    
    @RoomName  nvarchar(50),  
    @WeightTime varchar(20),  
    @CustomerName nvarchar(20),  
    @IsFirstSite varchar(1),  
    @Operator nvarchar(20),  
    @TaxObject varchar(1),  
    @EmptyCode varchar(20)  
   ) as
SET XACT_ABORT ON
begin tran trans   
   
--计算规费  
-----------------------------------------------------------------   
 Declare @TaxMoney decimal(14,4)    
 Declare @FundMoney decimal(14,4)    
 Declare @TaxGroup  numeric(10,0)  
 --判断扣规费的对象  
 Declare @TaxObjectName varchar(20)  
 if(@TaxObject='1')  
  Set @TaxObjectName=@RoomCode  
  else if(@TaxObject='2')     
  Set @TaxObjectName=@CollCode   
 else if(@TaxObject='3')
	Select  @TaxObjectName=ParcelCode from Sys_Colliery where CollCode=@CollCode
     
 Select @TaxMoney = 0.00    
 Select @FundMoney = 0.00   
 Set @TaxGroup=0  
   
 --GroupID  
 select @TaxGroup=isnull(max(TaxGroup),0) from TT_TaxItemDetail where EffectTime<=@WeightTime and RoomCode=@TaxObjectName and CoalKindCode=@CoalKindCode  
  
 --TaxMoney  
 select @TaxMoney=isnull(sum(Amount),0)  from TT_TaxItemDetail  
 where TaxGroup= @TaxGroup 
 and RoomCode=@TaxObjectName and CoalKindCode=@CoalKindCode  
  
 --FundMoney  
 select @FundMoney=isnull(sum(Amount),0)  from TT_TaxItemDetail  
 where TaxGroup= @TaxGroup 
 and RoomCode=@TaxObjectName and CoalKindCode=@CoalKindCode  
 and TaxItemCode in   
 (  
  select TaxItemCode from TT_TaxItem    
  where ItemType in(select distinct businID from SYS_Dictionary where businName like '%价格调节基金%')  
 )  
-----------------------------------------------------------------    
  --要扣除的金额  
 Declare @Tax decimal(14,4)   
  --要扣的价格调节基金  
 Declare @Fund decimal(14,4)   
 if(@IsFirstSite<>'1') --计算超重时要扣的税和基金  
  begin  
   Set @Tax=@TaxMoney*@OverWeight  
   Set @Fund=@FundMoney*@OverWeight  
  end  
 else --正常扣税时要扣的税和基金  
  begin  
   Set @Tax=@TaxMoney*@NetWeight  
   Set @Fund=@FundMoney*@NetWeight  
  end  

  --计算 余额-扣除税费 是否大于余额下限
 Declare @IsAcountLacking int
 Set @IsAcountLacking=0
 select @IsAcountLacking=count(0) from TT_ColieryAccount where (isnull(Account,0)-@Tax)>=isnull(LowAccount,0) and CollCode=@CollCode
 if(@IsAcountLacking=1)
 begin
	 Declare @CollCodeOld varchar(10)  
	 Declare @MarkedCardCodeOld varchar(32)  
	 Declare @TaxAmountOld decimal(14,4)    
	 
	 select @CollCodeOld=CollCode,@MarkedCardCodeOld=MarkedCardCode,@TaxAmountOld=TaxAmount from TT_LoadWeight 	 where WeightCode=@WeightCode   
	   
	  Declare @EndMoney decimal(14,4)  
	  Declare @TradeCode varchar(20)  
	  Declare @JournalCode int  

	 --修改重车过磅之前先备份
	 Insert TT_LoadWeightBackup select *,@Operator,getdate() from TT_LoadWeight where WeightCode=@WeightCode
	  
	 --修改重车过磅记录   
	 if(@IsFirstSite='1')  
	  begin  
	   Update TT_LoadWeight Set NavicertCode=@NavicertCode,MarkedCardCode=@MarkedCardCode,RemoteCardCode=@RemoteCardCode,  
	   CollCode=@CollCode,CollName=@CollName,CoalKindCode=@CoalKindCode,CoalKindName=@CoalKindName,CarOwnerName=@CarOwnerName,  
	   CarNo=@CarNo,CarType=@CarType,LoadWeight=@LoadWeight,EmptyWeight=@EmptyWeight,NetWeight=@NetWeight,OverWeight=0,  
	   TaxAmount=@Tax,FundAmount=@Fund,WeightTime=@WeightTime,  
	   RoomCode=@RoomCode,RoomName=@RoomName,CustomerName=@CustomerName,TaxGroup=@TaxGroup,EmptyCode=@EmptyCode  
	   where WeightCode=@WeightCode  
	  end  
	 else  
	  begin  
	   Update TT_LoadWeight Set NavicertCode=@NavicertCode,MarkedCardCode=@MarkedCardCode,RemoteCardCode=@RemoteCardCode,  
	   CollCode=@CollCode,CollName=@CollName,CoalKindCode=@CoalKindCode,CoalKindName=@CoalKindName,CarOwnerName=@CarOwnerName,  
	   CarNo=@CarNo,CarType=@CarType,LoadWeight=@LoadWeight,EmptyWeight=@EmptyWeight,NetWeight=0,OverWeight=@OverWeight,  
	   TaxAmount=@Tax,FundAmount=@Fund,WeightTime=@WeightTime,
	   RoomCode=@RoomCode,RoomName=@RoomName,CustomerName=@CustomerName,TaxGroup=@TaxGroup,EmptyCode=@EmptyCode  
	   where WeightCode=@WeightCode  
	  end  
	  
	  ---------------增加流水帐户记录--------------------------------------------  
	  Select @TradeCode=IsNull(Max(TradeCode)+1,1000000001) From TT_WaterBook  
	  Select @JournalCode=IsNull(Max(JournalCode)+1,1) From TT_WaterBook Where CollCode=@CollCodeOld And JournalCode Is Not Null  
	  Select @EndMoney = IsNull(Account,0.00) From TT_ColieryAccount Where CollCode=@CollCodeOld  
	    
	  Insert Into TT_WaterBook	  (TradeCode,CollCode,JournalCode,StartMoney,TradeMoney,EndMoney,Operator,TradeDate,TradeKind,WeightCode)  
	   Values(@TradeCode,@CollCodeOld,@JournalCode,@EndMoney,@TaxAmountOld,@EndMoney + @TaxAmountOld,@Operator,@WeightTime,'重车过磅修改',@WeightCode)  
	  --旧的煤矿账户增加  
	  Update TT_ColieryAccount Set Account=Account+@TaxAmountOld where CollCode=@CollCodeOld  
	  
	  Select @TradeCode=IsNull(Max(TradeCode)+1,1000000001) From TT_WaterBook  
	  Select @JournalCode=IsNull(Max(JournalCode)+1,1) From TT_WaterBook Where CollCode=@CollCode And JournalCode Is Not Null  
	  Select @EndMoney = IsNull(Account,0.00) From TT_ColieryAccount Where CollCode=@CollCode  
	    
	  Insert Into TT_WaterBook	  (TradeCode,CollCode,JournalCode,StartMoney,TradeMoney,EndMoney,Operator,TradeDate,TradeKind,WeightCode)  
	   Values(@TradeCode,@CollCode,@JournalCode,@EndMoney,-@Tax,@EndMoney - @Tax,@Operator,@WeightTime,'重车过磅修改',@WeightCode)  
	     
	  --新煤矿账户减少  
	  Update TT_ColieryAccount Set Account=Account-@Tax where CollCode=@CollCode  
	     
	 -------------------------------------------------------------------------------  
	 if(@MarkedCardCode<>'')  
	  begin  
	   --旧的标识卡状态改为未使用  
	   Update TT_MarkedCard set MarkedCardState='1' where MarkedCardCode=@MarkedCardCodeOld  
	    
	   --标识卡状态变为已使用(先修改旧的标识卡状态)  
	   Update TT_MarkedCard set MarkedCardState='2' where MarkedCardCode=@MarkedCardCode  
	  end  
	  
	 Select @WeightCode As WeightCode    
 end
 else
	Select -1 As WeightCode 
 IF(@@error=0)
	Commit tran trans
GO
