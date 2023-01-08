import * as React from "react";

import { useGetListProgramQuery, useGetFindProgramByIdQuery } from '../../store/services/AccessProgram'
import { IAccessProgram } from "../../store/types/AccessProgram";
import MainCard from "../../components/cards/MainCard";
import CardButton from "../../components/cards/CardButton";
import { LinearProgress } from "@mui/material";
import { IconButton } from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import EditIcon from "@mui/icons-material/Edit";
import { toast } from "react-toastify";
import { URL_API_V1 } from "../../store/services";
import { useNavigate } from "react-router-dom";
import Box from "@mui/material/Box";
import PublishedWithChangesIcon from '@mui/icons-material/PublishedWithChanges';
import SyncAltOutlinedIcon from '@mui/icons-material/SyncAltOutlined';
interface ListAccessProgramProps {}
const ListAccessProgram: React.FunctionComponent<ListAccessProgramProps> = () => {
  const { data: accessProgramData, error, isLoading } = useGetListProgramQuery();
  const navigate = useNavigate();
  
  const columnsDataGrid: GridColDef[] = [
    { field: "id", headerName: "ID", minWidth: 15, hide: true},
    { field: "name", headerName: "Name", minWidth: 170, flex:1  },
    { field: "url", headerName: "URL", minWidth: 170 , flex:1 },
    { field: "iconname", headerName: "Icon", minWidth: 100, flex:1,
      sortable: false,
      filterable:false,
      hideable:false,
      disableColumnMenu:true,
      renderCell: (params) => {
        return <div style={{display:"flex",alignItems:"center",justifyContent:"center",height:"100%",width:"100%"}}>
            <img style={{height:"90%"}} src={`${URL_API_V1}Programs/FindProgramsFileById/${params.row.id}`} alt={`${params.row.iconName}`} title={`${params.row.iconName}`} />
        </div>
        
      }
    },
    {
      field: "action",
      headerName: "Action",
      minWidth: 160,
      flex:1, 
      sortable: false,
      filterable:false,
      hideable:false,
      disableColumnMenu:true,
      renderCell: (params) => {
        const onClickChangedRol = (id: string, status: boolean) => {
          
        };
        const onClickEditUser = (id: string) => {
          navigate(`/access-program/${id}/edit`);
        };
        const onClickChangePasswordUser = (id: string) => {
          navigate(`/auth/change-password/${id}`);
        };
        
        return (
          <>
            <IconButton
              onClick={() => { onClickEditUser(params.row.id);}}
              color="secondary"
              title="Editar"
            >
            <EditIcon />
            </IconButton>
            <IconButton
              onClick={() => {
                onClickChangedRol(params.row.id,params.row.admin);
              }}
              color="secondary"    
              title="Delete"        >
              <SyncAltOutlinedIcon />           
            </IconButton>
          </>
        );
      },
    },
  ];

  return (
    <MainCard
      title="Access Program"
      secondary={
        <CardButton type="plus" title="Create Access" link="/access-program/create" />
      }
    >
      {isLoading ? (
        <LinearProgress color="secondary" />
      ) : (
        <Box sx={{ height: 400, width: "100%" }}>
          <DataGrid
            rows={accessProgramData !== undefined ? (accessProgramData as IAccessProgram[]) : []}
            columns={columnsDataGrid}
            pageSize={5}
            rowsPerPageOptions={[5]}
            disableSelectionOnClick
          />
        </Box>
      )}
    </MainCard>
  );
};

export default ListAccessProgram;
