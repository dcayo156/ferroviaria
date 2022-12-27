import * as React from "react";

import { useGetListCategoryQuery } from '../../store/services/Category'
import { ICategory,ICategoryWithParent } from "../../store/types/Category";
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
interface ListCategoryProps {}
const ListCategory: React.FunctionComponent<ListCategoryProps> = () => {
  const { data: categoryData, error, isLoading } = useGetListCategoryQuery();
  const navigate = useNavigate();
  
  const columnsDataGrid: GridColDef[] = [
    { field: "id", headerName: "ID", minWidth: 15, hide: true},
    { field: "name", headerName: "Nombre", minWidth: 170, flex:1  },
    { field: "parentCategory.name", headerName: "Categoria Padre", minWidth: 170 , flex:1,
      renderCell: (params) => {
        console.log(params)
      return (
        <>
          {params.row.parentCategory!=undefined?params.row.parentCategory.name:""}
        </>
      );
    }, },
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
          navigate(`//${id}/edit`);
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
      title="Categorias"
      secondary={
        <CardButton type="plus" title="Crear Categoria" link="/category/create" />
      }
    >
      {isLoading ? (
        <LinearProgress color="secondary" />
      ) : (
        <Box sx={{ height: 400, width: "100%" }}>
          <DataGrid
            rows={categoryData !== undefined ? (categoryData as ICategoryWithParent[]) : []}
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

export default ListCategory;
