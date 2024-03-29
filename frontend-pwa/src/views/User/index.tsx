import * as React from "react";
import { useGetListGetUsersQuery, useUpdateRoleAdminMutation} from "../../store/services/Auth";
import { IUpdateRoleAdminRequest, IUserResponse } from "../../store/types/Auth";
import MainCard from "../../components/cards/MainCard";
import CardButton from "../../components/cards/CardButton";
import { LinearProgress } from "@mui/material";
import { IconButton } from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import EditIcon from "@mui/icons-material/Edit";
import { toast } from "react-toastify";
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query";
import { SerializedError } from "@reduxjs/toolkit";
import { useNavigate } from "react-router-dom";
import Box from "@mui/material/Box";
import PublishedWithChangesIcon from '@mui/icons-material/PublishedWithChanges';
import SyncAltOutlinedIcon from '@mui/icons-material/SyncAltOutlined';
interface ListUserProps {}
const ListUser: React.FunctionComponent<ListUserProps> = () => {
  const { data: userData, error, isLoading } = useGetListGetUsersQuery();
  const navigate = useNavigate();
  const [changedRol] = useUpdateRoleAdminMutation();
  const columnsDataGrid: GridColDef[] = [
    { field: "id", headerName: "ID", minWidth: 15, hide: true},
    { field: "nombre", headerName: "Nombre", minWidth: 170, flex:1  },
    { field: "apellidos", headerName: "Apellidos", minWidth: 170 , flex:1 },
    { field: "username", headerName: "UserName", minWidth: 170, flex:1  },
    { field: "admin", headerName: "Administrador", minWidth: 170, flex:1, type:'boolean'},
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
          var mensaje = !status? "Seguro de volver Admnistrador a este Usuario":"Seguro de volver Operador a este Usuario";
          var bool = window.confirm(mensaje);
          const updateRoleAdminRequest : IUpdateRoleAdminRequest = {id:id, Status : !status};          
          bool &&
            changedRol(updateRoleAdminRequest).then((response:| { data: IUserResponse }| { error: FetchBaseQueryError | SerializedError }) => {
                if ("data" in response) {
                  toast.success(`Se cambio el Rol correctamente`);
                }
                if ("error" in response) {
                  toast.error("Error al momento de cambiar el Rol del usuario");
                }                
              }
            );
        };
        const onClickEditUser = (id: string) => {
          navigate(`/User/${id}/edit`);
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
            <PublishedWithChangesIcon
              onClick={() => { onClickChangePasswordUser(params.row.id);} }
              color="secondary"
              titleAccess="Cambiar contraseña"
            >
            </PublishedWithChangesIcon>
            <IconButton
              onClick={() => {
                onClickChangedRol(params.row.id,params.row.admin);
              }}
              color="secondary"    
              title="Cambiar Rol"        >
              <SyncAltOutlinedIcon />           
            </IconButton>
          </>
        );
      },
    },
  ];

  return (
    <MainCard
      title="Usuarios"
      secondary={
        <CardButton type="plus" title="Crear Usuario" link="/user/create" />
      }
    >
      {isLoading ? (
        <LinearProgress color="secondary" />
      ) : (
        <Box sx={{ height: 400, width: "100%" }}>
          <DataGrid
            rows={userData !== undefined ? (userData as IUserResponse[]) : []}
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

export default ListUser;
