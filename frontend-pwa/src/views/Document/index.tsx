import * as React from "react";

import { useGetListDocumentQuery, useDeleteDocumentMutation } from '../../store/services/Document'
import { IDocument, IDocumentOptions } from "../../store/types/Document";
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
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import DownloadOutlineIcon from '@mui/icons-material/DownloadOutlined';
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query";
import { SerializedError } from "@reduxjs/toolkit";
import { hasError } from "../../components/Security/ErrorManager";
import FormSelectOption from './Forms/FormSelectOption';
interface ListDocumentProps {}
const ListDocument: React.FunctionComponent<ListDocumentProps> = () => {
  const { data: documentData, error, isLoading } = useGetListDocumentQuery();
  const [deleteDocument]=useDeleteDocumentMutation();
  const navigate = useNavigate();
  const [options,setOptions]=React.useState<IDocumentOptions>({categoryId:"",subCategoryId:""});

  const columnsDataGrid: GridColDef[] = [
    { field: "id", headerName: "ID", minWidth: 15, hide: true},
    { field: "fileName", headerName: "Nombre File", minWidth: 200, flex:3  },
    { field: "filePath", headerName: "Donwload", minWidth: 50, flex:1,
      sortable: false,
      filterable:false,
      hideable:false,
      disableColumnMenu:true,
      renderCell: (params) => {
        return  <IconButton
                  download
                  target="_blank"
                  href={`${URL_API_V1}Documents/FindDocumentsFileById/${params.row.id}?isfile=true`}
                  color="secondary"    
                  title="Descargar">    
                  <DownloadOutlineIcon />           
                </IconButton>
      }
    },
    { field: "photoName", headerName: "Nombre Image", minWidth: 200, flex:3  },
    { field: "photoPath", headerName: "Donwload", minWidth: 50, flex:1,
      sortable: false,
      filterable:false,
      hideable:false,
      disableColumnMenu:true,
      renderCell: (params) => {
        return  <IconButton
                  disabled={params.row.photoPath==""}
                  download
                  target="_blank"
                  href={`${URL_API_V1}Documents/FindDocumentsFileById/${params.row.id}?isfile=false`}
                  color="secondary"    
                  title="Descargar">    
                  <DownloadOutlineIcon />           
                </IconButton>
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
        
          const onDeleteDocument = (id: string) => {
            deleteDocument(id).then((response: { data: string; } | { error: FetchBaseQueryError | SerializedError; })=>{
                if (hasError(response, "Error al momento de eliminar Documento")) {
                    return;
                }
                if ("data" in response) {
                    toast.success(`El Documento ha sido eliminado existosamente`);
                }
            });
         }
        
        const onClickEditUser = (id: string) => {
          navigate(`/document/${id}/edit`);
        };
        const onClickDeleteDocument = (id: string) => {
          const b=window.confirm("Esta seguro de eliminar este Documento?")
            if(b){
              onDeleteDocument(id);
            }else{
              toast.success("Operacion Cancelada")
            }
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
                onClickDeleteDocument(params.row.id);
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
      title= "Document"
      secondary={
        <CardButton type="plus" title="Create Document" link="/document/create" />
      }
    >
      {isLoading ? (
        <LinearProgress color="secondary" />
      ) : (
        <>
          <Box sx={{ height: 80, width: "100%" }}>
            <FormSelectOption document={options} setDocument={setOptions}/>
          </Box>
          <Box sx={{ height: 400, width: "100%" }}>
            <DataGrid
              rows={documentData !== undefined && options.subCategoryId!="" ? (documentData.filter(doc=>doc.subCategoryId==options.subCategoryId) as IDocument[]) : []}
              columns={columnsDataGrid}
              pageSize={5}
              rowsPerPageOptions={[5]}
              disableSelectionOnClick
            />
          </Box>
        </>
        
      )}
    </MainCard>
  );
};

export default ListDocument;
