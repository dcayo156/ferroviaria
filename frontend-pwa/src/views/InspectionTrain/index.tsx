import * as React from "react";

import { useGetListInspectionTrainsQuery, useDeleteInspectionTrainsMutation } from '../../store/services/InspectionTrain';
import { IInspectionTrain, IInspectionTrainOptions } from "../../store/types/InspectionTrain";
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
import FormSelectOption from './Forms/FormSelectOption2';
interface ListInspectionTrainsProps {}
const ListInspectionTrains: React.FunctionComponent<ListInspectionTrainsProps> = () => {
  const { data: InspectionTrainsData, error, isLoading } = useGetListInspectionTrainsQuery();
  const [deleteInspectionTrains]=useDeleteInspectionTrainsMutation();
  const navigate = useNavigate();
  const [options,setOptions]=React.useState<IInspectionTrainOptions>({categoryId:"",subCategoryId:""});

  const columnsDataGrid: GridColDef[] = [
    { field: "id", headerName: "ID", minWidth: 15, hide: true},
    { field: "codigo", headerName: "Código", minWidth: 200 , flex:  3},
    { field: "fileName", headerName: "Nombre File", minWidth: 200, flex:  2},
    { field: "observacionEvaluador", headerName: "Evaluador", minWidth: 200, flex:2  },
    { field: "filePath", headerName: "Descarga", minWidth: 50, flex:2,
      sortable: false,
      filterable:false,
      hideable:false,
      disableColumnMenu:true,
      renderCell: (params) => {
        return  <IconButton
                  download
                  target="_blank"
                  href={`${URL_API_V1}InspectionTrains/FindInspectionTrainsFileById/${params.row.id}`}
                  color="secondary"    
                  title="Descargar">    
                  <DownloadOutlineIcon />           
                </IconButton>
      }
    },
    { field: "Excel", headerName: "Lista Excel", minWidth: 50, flex:2,
    sortable: false,
    filterable:false,
    hideable:false,
    disableColumnMenu:true,
    renderCell: (params) => {
      return  <IconButton
                download
                target="_blank"
                href={`${URL_API_V1}InspectionTrains/GetInspectionTrainsAll`}
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
        const onDeleteInspectionTrains = (id: string) => {
            deleteInspectionTrains(id).then((response: { data: string; } | { error: FetchBaseQueryError | SerializedError; })=>{
                if (hasError(response, "Error al momento de eliminar InspectionTrainso")) {
                    return;
                }
                if ("data" in response) {
                    toast.success(`El InspectionTrainso ha sido eliminado existosamente`);
                }
            });
         }
        
        // const onClickEditUser = (id: string) => {
        //   navigate(`/InspectionTrains/${id}/edit`);
        // };
        const onClickDeleteInspectionTrains = (id: string) => {
          const b=window.confirm("Esta seguro de eliminar este InspectionTrainso?")
            if(b){
              onDeleteInspectionTrains(id);
            }else{
              toast.success("Operacion Cancelada")
            }
        };
        
        return (
          <>
            {/* <IconButton
              onClick={() => { onClickEditUser(params.row.id);}}
              color="secondary"
              title="Editar"
            >
            <EditIcon />
            </IconButton> */}
            <IconButton
              onClick={() => {
                onClickDeleteInspectionTrains(params.row.id);
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
      title= "Inspección integral de trenes"
      secondary={
        <CardButton type="plus" title="Crear Inspeccion de Tren" link="/InspectionTrain/create" />
      }
    >
      {isLoading ? (
        <LinearProgress color="secondary" />
      ) : (
        <>
          <Box sx={{ height: 80, width: "100%" }}>
            <FormSelectOption InspectionTrain={options} setInspectionTrain={setOptions}/>
          </Box>
          <Box sx={{ height: 400, width: "100%" }}>
            <DataGrid
              rows={InspectionTrainsData !== undefined && options.subCategoryId!="" ? (InspectionTrainsData.filter(doc=>doc.subCategoryId==options.subCategoryId) as IInspectionTrain[]) : []}
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

export default ListInspectionTrains;
