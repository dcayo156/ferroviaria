import * as React from "react";

import { useGetListCategoryQuery,useDeleteCategoryMutation } from '../../store/services/Category'
import { ICategory,ICategoryWithParent } from "../../store/types/Category";
import MainCard from "../../components/cards/MainCard";
import CardButton from "../../components/cards/CardButton";
import { LinearProgress } from "@mui/material";
import { IconButton } from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import EditIcon from "@mui/icons-material/Edit";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import Box from "@mui/material/Box";
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';
import { hasError } from '../../components/Security/ErrorManager';

interface ListCategoryProps {}
const ListCategory: React.FunctionComponent<ListCategoryProps> = () => {
  const { data: categoryData, error, isLoading } = useGetListCategoryQuery();
  const [ deleteCategory,
    { isLoading: isDeleting }, ] = useDeleteCategoryMutation();
  const navigate = useNavigate();
  const onDeleteCategory = (id: string) => {
    deleteCategory(id).then((response: { data: string; } | { error: FetchBaseQueryError | SerializedError; })=>{
        if (hasError(response, "Error al momento de eliminar categoria")) {
            return;
        }
        if ("data" in response) {
            toast.success(`La categoria ha sido eliminado existosamente`);
        }
    });
 }
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
        const onClickDeleteCategory = (id: string) => {
            const b=window.confirm("Esta seguro de eliminar esta categoria?")
            if(b){
              onDeleteCategory(id);
            }else{
              toast.success("Operacion Cancelada")
            }
        };
        const onClickEditUser = (id: string) => {
          navigate(`/category/${id}/edit`);
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
                onClickDeleteCategory(params.row.id);
              }}
              color="secondary"    
              title="Delete"        >
              <DeleteOutlineIcon />           
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
