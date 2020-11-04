# LinxServiceProxy
static int DTL_CALLBACK StatusProc (unsigned long conn_id,
                             unsigned long conn_param,
                             unsigned long state,
                             unsigned char *info,
                             unsigned long info_size);

static void DTL_CALLBACK PacketProc (unsigned long callback_param, unsigned long iostat);

		retval = DTL_INIT_RSI_EX(0, DTL_INIT_RSI_COOKIE, DTL_INIT_FLAGS_STARTUP_RSLINX | DTL_INIT_FLAGS_OPERATION_REFCOUNT);

m_pDtsapath = (DTSA_AB_CIP_PATH*) DTL_CreateDtsaFromPathString(pszNetworkAddress,&dwErr,DTL_FLAGS_ROUTE_TYPE_CIP);

retval = DTL_OpenDtsa((dtsa_type*)m_pDtsapath); //make sure to call close!


	cip_conn.ctype		= DTL_CONN_CIP;
    cip_conn.mode			= DTL_CIP_CONN_MODE_IS_CLIENT;
    cip_conn.trigger		= DTL_CIP_CONN_TRIGGER_APPLICATION;
    cip_conn.transport	= 3; //Class 3 connection
    cip_conn.tmo_mult		= 0;
    cip_conn.OT.conn_type	= DTL_CIP_CONN_TYPE_POINT_TO_POINT;
    cip_conn.OT.priority	= DTL_CIP_PRIORITY_LOW;
    cip_conn.OT.pkt_type	= DTL_CIP_CONN_PACKET_SIZE_VARIABLE;
    cip_conn.OT.pkt_size	= 500; //fix this, this is number of max bytes a packet can be
    cip_conn.OT.rpi		= 30000000L;
    cip_conn.OT.api		= 0L;
    cip_conn.TO.conn_type	= DTL_CIP_CONN_TYPE_POINT_TO_POINT;
    cip_conn.TO.priority	= DTL_CIP_PRIORITY_LOW;
    cip_conn.TO.pkt_type	= DTL_CIP_CONN_PACKET_SIZE_VARIABLE;
    cip_conn.TO.pkt_size	= 500;
    cip_conn.TO.rpi		= 30000000L;
    cip_conn.TO.api		= 0L;
	
	retval = DTL_CIP_CONNECTION_OPEN((DTSA_TYPE*)m_pDtsapath,&mr_ioi[0],&m_connectionID,m_listID,&cip_conn,NULL,(DTL_CIP_CONNECTION_STATUS_PROC )CCipConnectionManager::StatusProc,10000L);

DTL_UNINIT(DTL_E_FAIL);

	DWORD dwNumDrivers;
	void* pDriverList;
	int driver_id = -1;
	DTL_RETVAL retval;

	// Ask DTL to create a list of drivers.
	pDriverList = DTL_CreateDriverList(&dwNumDrivers,5000UL);

    		int index;
		void* pDriverEntry;

		// Walk thru every driver entry in the list.
		for(index = 0; index < (int)dwNumDrivers; index++)
		{
			pDriverEntry = DTL_GetDriverListEntryFromDriverListIndex(pDriverList,index);

            	char szDrivername[DTL_DRIVER_NAME_MAX];
				strcpy_s(szDrivername,DTL_GetDriverNameFromDriverListEntry(pDriverEntry));

				
				if(strncmp(driverName,szDrivername,DTL_DRIVER_NAME_MAX) == 0)
				{
					driver_id = DTL_GetHandleFromDriverListEntry(pDriverEntry);
					
					retval = DTL_DRIVER_OPEN(driver_id,driverName,10000L);
                    		DTL_DestroyDriverList(pDriverList,5000UL);

	cip_conn.ctype		= DTL_CONN_CIP;
    cip_conn.mode			= DTL_CIP_CONN_MODE_IS_CLIENT;
    cip_conn.trigger		= DTL_CIP_CONN_TRIGGER_APPLICATION;
    cip_conn.transport	= 3; //Class 3 connection
    cip_conn.tmo_mult		= 0;
    cip_conn.OT.conn_type	= DTL_CIP_CONN_TYPE_POINT_TO_POINT;
    cip_conn.OT.priority	= DTL_CIP_PRIORITY_LOW;
    cip_conn.OT.pkt_type	= DTL_CIP_CONN_PACKET_SIZE_VARIABLE;
    cip_conn.OT.pkt_size	= 500; //fix this, this is number of max bytes a packet can be
    cip_conn.OT.rpi		= 30000000L;
    cip_conn.OT.api		= 0L;
    cip_conn.TO.conn_type	= DTL_CIP_CONN_TYPE_POINT_TO_POINT;
    cip_conn.TO.priority	= DTL_CIP_PRIORITY_LOW;
    cip_conn.TO.pkt_type	= DTL_CIP_CONN_PACKET_SIZE_VARIABLE;
    cip_conn.TO.pkt_size	= 500;
    cip_conn.TO.rpi		= 30000000L;
    cip_conn.TO.api		= 0L;
	
	retval = DTL_CIP_CONNECTION_OPEN((DTSA_TYPE*)m_pDtsapath,&mr_ioi[0],&m_connectionID,m_listID,&cip_conn,NULL,(DTL_CIP_CONNECTION_STATUS_PROC )CCipConnectionManager::StatusProc,10000L);

		retval = DTL_CIP_CONNECTION_CLOSE(m_connectionID,5000L);

DTL_CloseDtsa((dtsa_type*)m_pDtsapath); //this will call free

		if(m_ConnectionType==eCONNECTION_TYPE_BINARY)
		{
			DTL_DRIVER_CLOSE(m_pDtsapath->driver_id, 1000);
			free(m_pDtsapath);
		}
		else if(m_ConnectionType==eCONNECTION_TYPE_PATH) 
		{
			DTL_DestroyDtsa((dtsa_type*) m_pDtsapath);
		}

	retval = DTL_CIP_MESSAGE_SEND_CB((dtsa_type*)&m_dtsaConn,svc_code, ioi_path,databuf, dataLength,pEvent->data,&pEvent->dsize,pEvent->data,&pEvent->esize,10000L,(DTL_IO_CALLBACK_PROC)CCipConnectionManager::PacketProc,(ULONG) pEvent);


