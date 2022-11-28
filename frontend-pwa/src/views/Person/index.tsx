import * as React from "react";
import {
  useGetListPeopleQuery,
  useDeletePeopleMutation,
} from "../../store/services/Person";
import { IPerson } from "../../store/types/Person";
import MainCard from "../../components/cards/MainCard";
import CardButton from "../../components/cards/CardButton";
import { LinearProgress } from "@mui/material";
import { IconButton } from "@mui/material";
import { DataGrid, GridColDef, GridValueGetterParams } from "@mui/x-data-grid";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import FamilyRestroomIcon from "@mui/icons-material/FamilyRestroom";
import { toast } from "react-toastify";
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query";
import { SerializedError } from "@reduxjs/toolkit";
import { useNavigate } from "react-router-dom";
import Box from "@mui/material/Box";

interface ListPeopleProps {}
const CalculateAge = (birthday: Date) => {
  var today:Date = new Date();
  //var birthday = date;
  var age = today.getFullYear() - birthday.getFullYear();
  var m = today.getMonth() - birthday.getMonth();
  if (m < 0 || (m === 0 && today.getDate() < birthday.getDate())) {
      age--;
  }
  return age;
}
const ListPeople: React.FunctionComponent<ListPeopleProps> = () => {
  const { data: people, error, isLoading } = useGetListPeopleQuery();
  const navigate = useNavigate();
  const [deletePerson, { isLoading: isUpdating }] = useDeletePeopleMutation();

  const columnsDataGrid: GridColDef[] = [
    { field: "id", headerName: "ID", minWidth: 15, hide: true},
    { field: "firstName", headerName: "Primer Nombre", minWidth: 170, flex:1  },
    { field: "secondName", headerName: "Segundo Nombre", minWidth: 170 , flex:1 },
    { field: "lastName", headerName: "Apellido", minWidth: 170, flex:1  },
    {
      field: "birthDate",
      headerName: "Edad",
      type: "number",
      flex:1,
      minWidth: 170,
      valueGetter: (params: GridValueGetterParams) =>
        CalculateAge(new Date(params.row.birthDate)),
    },
    { field: "gender", headerName: "Genero", minWidth: 160 },
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
        const onClickDeletePerson = (id: string) => {
          var bool = window.confirm("Seguro de eliminar esta Persona?");
          bool &&
            deletePerson(id).then(
              (
                response:
                  | { data: string }
                  | { error: FetchBaseQueryError | SerializedError }
              ) => {
                if ("data" in response) {
                  toast.success(`Persona eliminada correctamente`);
                }
                if ("error" in response) {
                  toast.error("Error al momento de Eliminar la persona");
                }
              }
            );
        };
        const onClickEditPerson = (id: string) => {
          navigate(`/person/${id}/edit`);
        };
        const onClickRelationshipPerson = (id: string) => {
          navigate(
            `/Relationship/${id}/${params.row.firstName} ${params.row.lastName}`
          );
        };

        return (
          <>
            <IconButton
              onClick={() => {
                onClickEditPerson(params.row.id);
              }}
              color="secondary"
            >
              <EditIcon />
            </IconButton>
            <IconButton
              onClick={() => {
                onClickDeletePerson(params.row.id);
              }}
              color="secondary"
            >
              <DeleteIcon />
            </IconButton>
            <IconButton
              onClick={() => {
                onClickRelationshipPerson(params.row.id);
              }}
              color="secondary"
            >
              <FamilyRestroomIcon />
            </IconButton>
          </>
        );
      },
    },
  ];

  return (
    <MainCard
      title="Personas"
      secondary={
        <CardButton type="plus" title="Crear Persona" link="/person/createSimple" />
      }
    >
      {isLoading ? (
        <LinearProgress color="secondary" />
      ) : (
        <Box sx={{ height: 400, width: "100%" }}>
          <DataGrid
            rows={people !== undefined ? (people as IPerson[]) : []}
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

export default ListPeople;
