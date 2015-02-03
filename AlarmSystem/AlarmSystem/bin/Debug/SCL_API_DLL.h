// *************************************************************************
// ���ļ��ṩ���δ���, �ֱ𿽱����û������<����>��<��ʼ��>��<�˳�>�����ط�
// This file included three parts of source code, they must be inserted to
// 'define' 'initial' and 'exit' code of your program .
// *************************************************************************

// **************************************************************************
// ��һ����: ����״̬�Ľṹ�Ͷ�̬���ӿ��ṩ�ĺ���ָ�붨��, ��ӵ� C++ ���ඨ���� 
// ����ѡ���� LIBRARY_CALL_MODE Ϊ MODE_STDCALL �� MODE_CDECL ��ѡ��̬���ӿ�
// �ĺ�������ģʽ
// Part 1: Some structure and all pointer of the functions in Dll
// Please insert them to defination of your class.
// The 'LIBRARY_CALL_MODE' can be defined as 'MODE_STDCALL' or 'MODE_CDECL' to
// choose the call mode of the DLL
// **************************************************************************

#define     LIBRARY_CALL_MODE      MODE_STDCALL

#define     MODE_STDCALL    0
#define     MODE_CDECL      1

/* FAT16���ļ�Ŀ¼����
   define of directory item of FAT16
struct _DIR_INFO
{
	char	Name[8];		// �Կո���չ�����ļ���,�޽�����       , Filename
	char	Ext[3];			// �Կո���չ������չ��,�޽�����       , Extension
	BYTE	Attr;			// ����, 0x00��ʾ��ͨ�ļ�, 0x10��ʾ��Ŀ¼, Attribute,0x00:Normal file, 0x10:Sub-directary
	BYTE	Reserved1[10];	// ����                                  , Reserved
	WORD	Time;			// �ļ�������ʱ��,ר�Ÿ�ʽ               , Time when the file created, special format.
	WORD	Date;			// �ļ�����������,ר�Ÿ�ʽ               , Date when the file created, special format.
	WORD	Reserved2;		// ����                                  , Reserved
	UINT	Length;			// ���ֽ�Ϊ��λ���ļ�����                , File length in bytes.
};
*/

// ����״̬�ṹ���Ͷ���
// Structure of runtime-information
struct	RunTimeInfoStru
{
    WORD	Start[ 15];			// �޹�����      , Reserved.
	WORD	TotalProgCount;		// �ܽ�Ŀ��      , Total program count.
	WORD	CurrentProg;		// ��ǰ���ŵĽ�Ŀ, Which program is playing in current programs.
	WORD	NotUsed1;			// �޹�����      , Reserved.
	WORD	ProgDrv;            // ��Ŀ����������, Disk number of the program from
	WORD	SD_OK;              // SD��������־  , Ready Flag of the SD card.
	WORD	NotUsed2;           // ����          , Reserved.
	WORD	Humid;              // ʪ��          , Humidity from the sensor
	short	Temprature;         // �¶�          , Tempeature from DS18B20
	WORD	PowerSwitch;        // �����Դ      , State of the power supply of LED.
	int     NotUsed3;           // ����          , Reserved.
	BYTE	ProgramIndex;       // ��Ŀ����      , Which set of program now playing.
	BYTE	ProgramDrv;         // ��Ŀ������    , Such as ProgDrv
	WORD	NotUsed4[8];        // ����          , Reserved.
	WORD	Second;             // ����оƬ֮��  , Second of the RTC in the controller.
	WORD	Minute;             // ����оƬ֮��  , Minute of the RTC in the controller.
	WORD	Hour;               // ����оƬ֮ʱ  , Hour   of the RTC in the controller.
	WORD	Day;                // ����оƬ֮��  , Day of month of the RTC in the controller.
	WORD	Month;              // ����оƬ֮��  , Month  of the RTC in the controller.
	WORD	Week;               // ����оƬ֮����, Day of week of the RTC in the controller.
	WORD	Year;               // ����оƬ֮��  , Year   of the RTC in the controller.
	WORD	Brightness;         // ����          , Brightness set to LED.
	WORD	NotUsed5;           // ����          , Reserved.
	char	Com1Data[8][8];     // COM1���յ�����, 8 groups of data received from serial-port 1
	char	Com2Data[8][8];     // COM2���յ�����, 8 groups of data received from serial-port 2
	char	Com3Data[8][8];	    // COM3���յ�����, 8 groups of data received from serial-port 3
	int	    NotUsed6[24];       // ����          , Reserved.
	WORD	NotUsed7;           // ����          , Reserved.
	WORD	PowerMode;          // ��Դģʽ      , Mode of Power supply of LED.
	WORD	NotUsed8[7];        // ����          , Reserved.
	WORD	SW1;                // SW1��״̬     , State of the SW1 port.
	WORD	SW2;                // SW2��״̬     , State of the SW2 port
	WORD	NotUsed9[57];       // ����          , Reserved.
};
typedef	struct RunTimeInfoStru	RunTimeInfo;

struct	TextOutInfo
{
	WORD	Left;		// ������   , Left coordinate of the area.
	WORD	Top;		// ������   , Top  coordinate of the area.
	WORD	Width;		// �����   , Width  of the area.
	WORD	Height;		// �����   , Height of the area.
	LONG	Color;		// ��ɫ     , Text color
	WORD	ASCFont;	// Ӣ������ , Font index of ASCII charactors.
	WORD	HZFont;		// �������� , Font index of Local language charactors.
	WORD	XPos;		// x����    , X coordinate for output text.
	WORD	YPos;		// y����    , Y coordinate for output text.
};
typedef struct TextOutInfo	TEXTINFO;

#if (LIBRARY_CALL_MODE==MODE_STDCALL)
#define LIBRARY_MODE    __stdcall
#else   
#define LIBRARY_MODE    __cdecl
#endif

typedef BOOL (FAR LIBRARY_MODE *_SCL_NetInitial    )(WORD mDevID,char *Password,char *RemoteIP,int SecTimeOut,int RetryTimes,WORD UDPPort,BOOL bSCL2008);
typedef BOOL (FAR LIBRARY_MODE *_SCL_ComInitial    )(WORD mDevID,int ComPort,int Baudrate,int LedNum,int SecTimeOut,int RetryTimes,BOOL bSCL2008);
typedef BOOL (FAR LIBRARY_MODE *_SCL_Close         )(WORD mDevID);
typedef BOOL (FAR LIBRARY_MODE *_SCL_SetRemoteIP   )(WORD mDevID,char *RemoteIP);
typedef BOOL (FAR LIBRARY_MODE *_SCL_SetLEDNum     )(WORD mDevID,int  LedNum);
typedef BOOL (FAR LIBRARY_MODE *_SCL_TargetSCL2008 )(WORD mDevID,BOOL b2008);
typedef BOOL (FAR LIBRARY_MODE *_SCL_GetLastResult )(WORD mDevID);

typedef BOOL (FAR LIBRARY_MODE *_SCL_InitForPackage)(WORD mDevID, BOOL bNet, BOOL bSCL2008);
typedef int  (FAR LIBRARY_MODE *_SCL_GetPackage    )(WORD mDevID,BYTE *Data, int *AnswerCount);
typedef BOOL (FAR LIBRARY_MODE *_SCL_CheckAnswer   )(WORD mDevID,int *AnswerCount, BYTE *AnswerData);

typedef BOOL (FAR LIBRARY_MODE *_SCL_SendFile      )(WORD mDevID,int Drv,char *Path,char *LocalFilename);
typedef int  (FAR LIBRARY_MODE *_SCL_ReceiveFile   )(WORD mDevID,int Drv,char *RemoteFileName,char *LocalFilename);
typedef BOOL (FAR LIBRARY_MODE *_SCL_FormatDisk    )(WORD mDevID,int drv);
typedef int  (FAR LIBRARY_MODE *_SCL_FreeSpace     )(WORD mDevID,int Drv);
typedef int  (FAR LIBRARY_MODE *_SCL_DirItemCount  )(WORD mDevID,int Drv,char *Path);
typedef BOOL (FAR LIBRARY_MODE *_SCL_GetDirItem    )(WORD mDevID,int ItemCount,void *Buff);
typedef BOOL (FAR LIBRARY_MODE *_SCL_RemoveFile    )(WORD mDevID,int Drv,char *FileName);
typedef BOOL (FAR LIBRARY_MODE *_SCL_MD            )(WORD mDevID,int Drv,char *PathName);
typedef BOOL (FAR LIBRARY_MODE *_SCL_RD            )(WORD mDevID,int Drv,char *PathName);
typedef LONG (FAR LIBRARY_MODE *_SCL_LoadFile      )(WORD mDevID,int DrvNo,char *FileName);
typedef BOOL (FAR LIBRARY_MODE *_SCL_SaveFile      )(WORD mDevID,int DrvNo,char *FileName,int Len,int Date,int Time);
typedef BOOL (FAR LIBRARY_MODE *_SCL_SendData      )(WORD mDevID,int Offset,int SendBytes,BYTE *Buff);
typedef BOOL (FAR LIBRARY_MODE *_SCL_ReceiveData   )(WORD mDevID,int Offset,int ReadBytes,BYTE *Buff);
typedef BOOL (FAR LIBRARY_MODE *_SCL_ShowString    )(WORD mDevID,short *Info, char *Str);
typedef BOOL (FAR LIBRARY_MODE *_SCL_SendSmallFile )(WORD mDevID,int DrvNo,char *FileName,int Len,int Date,int Time,
							WORD RestartFlag, WORD RestartDrv, WORD RestartIndex,BYTE *data);

typedef BOOL (FAR LIBRARY_MODE *_SCL_Reset         )(WORD mDevID);
typedef BOOL (FAR LIBRARY_MODE *_SCL_Replay        )(WORD mDevID,int Drv,int PlayListIndex);
typedef BOOL (FAR LIBRARY_MODE *_SCL_SetTimer      )(WORD mDevID);
typedef BOOL (FAR LIBRARY_MODE *_SCL_SetBright     )(WORD mDevID,short Brightness);
typedef BOOL (FAR LIBRARY_MODE *_SCL_SetOnOffTime  )(WORD mDevID,short OnTime,short OffTime);
typedef BOOL (FAR LIBRARY_MODE *_SCL_SetTempOffset )(WORD mDevID,short Offset);
typedef BOOL (FAR LIBRARY_MODE *_SCL_SetPowerMode  )(WORD mDevID,int PowerMode);
typedef BOOL (FAR LIBRARY_MODE *_SCL_GetRunTimeInfo)(WORD mDevID,void *Buff512Bytes);
typedef BOOL (FAR LIBRARY_MODE *_SCL_GetPlayInfo   )(WORD mDevID,BYTE *PlayInfo);
typedef BOOL (FAR LIBRARY_MODE *_SCL_LedShow       )(WORD mDevID,BOOL OnOff);
typedef BOOL (FAR LIBRARY_MODE *_SCL_SetExtSW      )(WORD mDevID,WORD OnOff);

typedef BOOL (FAR LIBRARY_MODE *_SCL_PictToXMPFile )(int ColorType,int Width,int Height,BOOL bStretched,char *PictFileName,char *XMPFileName);
typedef int  (FAR LIBRARY_MODE *_SCL_GetMaxFileSize)(int TotalBuffCount,BOOL bSmallest);
typedef int  (FAR LIBRARY_MODE *_SCL_AddXMPToXMP   )(char *InFileName,char *OutFileName,int BuffSize);
typedef BOOL (FAR LIBRARY_MODE *_SCL_GetFileDosDateTime)(char *Name,int *Date,int *Time);

typedef BOOL (FAR LIBRARY_MODE *_SCL_SeekStart     )(WORD delay, char *IP, WORD port, BOOL bSCL2008);
typedef BOOL (FAR LIBRARY_MODE *_SCL_SeekGetAItem  )(char *IP,char *Name);
typedef BOOL (FAR LIBRARY_MODE *_SCL_SeekClose     )(void);

typedef BOOL (FAR LIBRARY_MODE *_SCL_Init_Start    )(WORD delay, char *IP, WORD port, BOOL bSCL2008);
typedef BOOL (FAR LIBRARY_MODE *_SCL_Init_Get      )(BYTE *NetParaInfo);
typedef BOOL (FAR LIBRARY_MODE *_SCL_Init_Set      )(BYTE *NetParaInfo);
typedef BOOL (FAR LIBRARY_MODE *_SCL_Init_Close    )(void);

_SCL_NetInitial		    SCL_NetInitial;
_SCL_ComInitial		    SCL_ComInitial;
_SCL_Close			    SCL_Close;
_SCL_SetRemoteIP	    SCL_SetRemoteIP;
_SCL_SetLEDNum		    SCL_SetLEDNum;
_SCL_TargetSCL2008      SCL_TargetSCL2008;
_SCL_GetLastResult		SCL_GetLastResult;

_SCL_InitForPackage     SCL_InitForPackage;
_SCL_GetPackage	        SCL_GetPackage;
_SCL_CheckAnswer	    SCL_CheckAnswer;

_SCL_FormatDisk		    SCL_FormatDisk;
_SCL_FreeSpace		    SCL_FreeSpace;
_SCL_DirItemCount	    SCL_DirItemCount;
_SCL_GetDirItem		    SCL_GetDirItem;
_SCL_SendFile		    SCL_SendFile;
_SCL_ReceiveFile	    SCL_ReceiveFile;
_SCL_RemoveFile		    SCL_RemoveFile;
_SCL_MD				    SCL_MD;
_SCL_RD				    SCL_RD;
_SCL_LoadFile			SCL_LoadFile;
_SCL_SaveFile			SCL_SaveFile;
_SCL_SendData			SCL_SendData;
_SCL_ReceiveData		SCL_ReceiveData;
_SCL_ShowString		    SCL_ShowString;
_SCL_SendSmallFile	SCL_SendSmallFile;

_SCL_Reset			    SCL_Reset;
_SCL_Replay			    SCL_Replay;
_SCL_SetTimer		    SCL_SetTimer;
_SCL_SetBright		    SCL_SetBright;
_SCL_SetOnOffTime	    SCL_SetOnOffTime;
_SCL_SetTempOffset	    SCL_SetTempOffset;
_SCL_SetPowerMode	    SCL_SetPowerMode;
_SCL_GetRunTimeInfo	    SCL_GetRunTimeInfo;
_SCL_GetPlayInfo	    SCL_GetPlayInfo;
_SCL_LedShow			SCL_LedShow;
_SCL_SetExtSW			SCL_SetExtSW;

_SCL_PictToXMPFile	    SCL_PictToXMPFile;
_SCL_GetMaxFileSize	    SCL_GetMaxFileSize;
_SCL_AddXMPToXMP	    SCL_AddXMPToXMP;
_SCL_GetFileDosDateTime	SCL_GetFileDosDateTime;

_SCL_SeekStart          SCL_SeekStart
_SCL_SeekGetAItem       SCL_SeekGetAItem
_SCL_SeekClose          SCL_SeekClose

_SCL_Init_Start         SCL_Init_Start
_SCL_Init_Get           SCL_Init_Get
_SCL_Init_Set           SCL_Init_Set
_SCL_Init_Close         SCL_Init_Close

HINSTANCE				hSCL_Dll;

/* **************************************************************************
// �ڶ�����: ����Ϊ���������ж�̬���ӿ�ĳ�ʼ������, ��ӵ��û�����ĳ�ʼ��������
// Part 2: Initial for the DLL, Please inserted them to initial part of your program.
// **************************************************************************

	// ���붯̬���ӿ�
    // Load DLL
    #if (LIBRARY_CALL_MODE==MODE_STDCALL)
	hSCL_Dll = LoadLibrary("SCL_API_stdcall.Dll");
    #else
    #if  (LIBRARY_CALL_MODE==MODE_CDECL)
	hSCL_Dll = LoadLibrary("SCL_API_cdecl.Dll");
    #else hSCL_Dll=NULL;
    #endif
    #endif

	// ��ȡ�ɵ��õĺ�����ָ��
    // Get pointer of these functions in the DLL
	if (hSCL_Dll!=NULL)
	{
		SCL_NetInitial     = (_SCL_NetInitial    )GetProcAddress(hSCL_Dll,"SCL_NetInitial"    );
		SCL_ComInitial     = (_SCL_ComInitial    )GetProcAddress(hSCL_Dll,"SCL_ComInitial"    );
		SCL_Close          = (_SCL_Close         )GetProcAddress(hSCL_Dll,"SCL_Close"         );
		SCL_SetRemoteIP    = (_SCL_SetRemoteIP   )GetProcAddress(hSCL_Dll,"SCL_SetRemoteIP"   );
		SCL_SetLEDNum      = (_SCL_SetLEDNum     )GetProcAddress(hSCL_Dll,"SCL_SetLEDNum"     );
        SCL_TargetSCL2008  = (_SCL_TargetSCL2008 )GetProcAddress(hSCL_Dll,"SCL_TargetSCL2008" );
		SCL_GetLastResult  = (_SCL_GetLastResult )GetProcAddress(hSCL_Dll,"SCL_GetLastResult" );

        SCL_InitForPackage = (_SCL_InitForPackage)GetProcAddress(hSCL_Dll,"SCL_InitForPackage");
		SCL_GetPackage     = (_SCL_GetPackage    )GetProcAddress(hSCL_Dll,"SCL_GetPackage"    );
		SCL_CheckAnswer    = (_SCL_CheckAnswer   )GetProcAddress(hSCL_Dll,"SCL_CheckAnswer"   );

        SCL_FormatDisk     = (_SCL_FormatDisk    )GetProcAddress(hSCL_Dll,"SCL_FormatDisk"    );
		SCL_FreeSpace      = (_SCL_FreeSpace     )GetProcAddress(hSCL_Dll,"SCL_FreeSpace"     );
		SCL_DirItemCount   = (_SCL_DirItemCount  )GetProcAddress(hSCL_Dll,"SCL_DirItemCount"  );
		SCL_GetDirItem     = (_SCL_GetDirItem    )GetProcAddress(hSCL_Dll,"SCL_GetDirItem"    );
		SCL_SendFile       = (_SCL_SendFile      )GetProcAddress(hSCL_Dll,"SCL_SendFile"      );
		SCL_ReceiveFile    = (_SCL_ReceiveFile   )GetProcAddress(hSCL_Dll,"SCL_ReceiveFile"   );
		SCL_RemoveFile     = (_SCL_RemoveFile    )GetProcAddress(hSCL_Dll,"SCL_RemoveFile"    );
		SCL_MD             = (_SCL_MD            )GetProcAddress(hSCL_Dll,"SCL_MD"            );
		SCL_RD             = (_SCL_RD            )GetProcAddress(hSCL_Dll,"SCL_RD"            );
		SCL_SaveFile       = (_SCL_SaveFile      )GetProcAddress(hSCL_Dll,"SCL_SaveFile"      );
		SCL_LoadFile       = (_SCL_LoadFile      )GetProcAddress(hSCL_Dll,"SCL_LoadFile"      );
		SCL_SendData       = (_SCL_SendData      )GetProcAddress(hSCL_Dll,"SCL_SendData"      );
		SCL_ReceiveData    = (_SCL_ReceiveData   )GetProcAddress(hSCL_Dll,"SCL_ReceiveData"   );
		SCL_SendSmallFile  = (_SCL_SendSmallFile )GetProcAddress(hSCL_Dll,"SCL_SendSmallFile" );
        SCL_ShowString     = (_SCL_ShowString    )GetProcAddress(hSCL_Dll,"SCL_ShowString"    );

        SCL_Reset          = (_SCL_Reset         )GetProcAddress(hSCL_Dll,"SCL_Reset"         );
		SCL_Replay         = (_SCL_Replay        )GetProcAddress(hSCL_Dll,"SCL_Replay"        );
		SCL_SetTimer       = (_SCL_SetTimer      )GetProcAddress(hSCL_Dll,"SCL_SetTimer"      );
		SCL_SetBright      = (_SCL_SetBright     )GetProcAddress(hSCL_Dll,"SCL_SetBright"     );
		SCL_SetOnOffTime   = (_SCL_SetOnOffTime  )GetProcAddress(hSCL_Dll,"SCL_SetOnOffTime"  );
		SCL_SetTempOffset  = (_SCL_SetTempOffset )GetProcAddress(hSCL_Dll,"SCL_SetTempOffset" );
		SCL_SetPowerMode   = (_SCL_SetPowerMode  )GetProcAddress(hSCL_Dll,"SCL_SetPowerMode"  );
		SCL_GetRunTimeInfo = (_SCL_GetRunTimeInfo)GetProcAddress(hSCL_Dll,"SCL_GetRunTimeInfo");
		SCL_LedShow        = (_SCL_LedShow       )GetProcAddress(hSCL_Dll,"SCL_LedShow"       );
		SCL_SetExtSW       = (_SCL_SetExtSW      )GetProcAddress(hSCL_Dll,"SCL_SetExtSW"      );
		SCL_GetPlayInfo    = (_SCL_GetPlayInfo   )GetProcAddress(hSCL_Dll,"SCL_GetPlayInfo"   );

        SCL_PictToXMPFile  = (_SCL_PictToXMPFile )GetProcAddress(hSCL_Dll,"SCL_PictToXMPFile" );
		SCL_GetMaxFileSize = (_SCL_GetMaxFileSize)GetProcAddress(hSCL_Dll,"SCL_GetMaxFileSize");
		SCL_AddXMPToXMP    = (_SCL_AddXMPToXMP   )GetProcAddress(hSCL_Dll,"SCL_AddXMPToXMP"   );
		SCL_GetFileDosDateTime = (_SCL_GetFileDosDateTime)GetProcAddress(hSCL_Dll,"SCL_GetFileDosDateTime");

		SCL_SeekStart      = (_SCL_SeekStart     )GetProcAddress(hSCL_Dll,"SCL_SeekStart");
		SCL_SeekGetAItem   = (_SCL_SeekGetAItem  )GetProcAddress(hSCL_Dll,"SCL_SeekGetAItem");
		SCL_SeekClose      = (_SCL_SeekClose     )GetProcAddress(hSCL_Dll,"SCL_SeekClose");

		SCL_Init_Start     = (_SCL_Init_Start    )GetProcAddress(hSCL_Dll,"SCL_Init_Start");
		SCL_Init_Get       = (_SCL_Init_Get      )GetProcAddress(hSCL_Dll,"SCL_Init_Get");
		SCL_Init_Set       = (_SCL_Init_Set      )GetProcAddress(hSCL_Dll,"SCL_Init_Set");
		SCL_Init_Close     = (_SCL_Init_Close    )GetProcAddress(hSCL_Dll,"SCL_Init_Close");

	}

	// �жϺ���ָ���ȡ�ɹ���
    // Check these pointer
	if ((!hSCL_Dll	   )||
		(!SCL_NetInitial    )||
        (!SCL_ComInitial    )||
        (!SCL_Close         )||
        (!SCL_SetLEDNum     )||
        (!SCL_SetRemoteIP   )||
        (!SCL_TargetSCL2008 )||
        (!SCL_GetLastResult )||

        (!SCL_InitForPackage)||
        (!SCL_GetPackage    )||
        (!SCL_CheckAnswer   )||

        (!SCL_FormatDisk    )||
        (!SCL_FreeSpace     )||
		(!SCL_DirItemCount  )||
        (!SCL_GetDirItem    )||
        (!SCL_SendFile      )||
        (!SCL_ReceiveFile   )||
		(!SCL_RemoveFile    )||
        (!SCL_MD            )||
        (!SCL_RD            )||
        (!SCL_SaveFile      )||
        (!SCL_LoadFile      )||
		(!SCL_SendData      )||
        (!SCL_ReceiveData   )||
	(!SCL_SendSmallFile )||
        (!SCL_ShowString    )||

		(!SCL_Reset         )||
        (!SCL_Replay        )||
        (!SCL_SetTimer      )||
        (!SCL_SetBright     )||
		(!SCL_SetOnOffTime  )||
        (!SCL_SetTempOffset )||
        (!SCL_SetPowerMode  )||
		(!SCL_GetRunTimeInfo)||
        (!SCL_GetPlayInfo   )||
        (!SCL_LedShow       )||
        (!SCL_SetExtSW      )||

        (!SCL_PictToXMPFile )||
        (!SCL_GetMaxFileSize)||
        (!SCL_AddXMPToXMP   )||
		(!SCL_GetFileDosDateTime)		
	   )
	{
		FreeLibrary(hSCL_Dll);
		hSCL_Dll = NULL;
		return 9999;
	}

/* **************************************************************************
// ��������: ����Ϊ��̬���ӿ���ͷŴ���, ��ӵ��û�������˳�������
// Part 3 : Exit code for the DLL
// Please inserted them to the exit part of your program.
// **************************************************************************

	if (hSCL_Dll!=NULL) FreeLibrary(hSCL_Dll);

*/ // ***********************************************************************
