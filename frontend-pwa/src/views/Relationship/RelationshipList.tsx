import TextField from "@mui/material/TextField";
import Autocomplete from "@mui/material/Autocomplete";

import { useState } from "react";
import { useParams } from "react-router-dom";
import MainCard from "../../components/cards/MainCard";
import {
    useCreateRelationshipMutation,
  useDeleteRelationshipMutation,
  useGetRelationShipsByPersonIdQuery,
  useGetRelationShipTypesQuery,
} from "../../store/services/Relationship";
import {
    IRelationShip,
  IRelationshipByPerson 
} from "../../store/types/delete-Relationship";
import CardLink from "../../components/cards/CardLink";
import DialogForm from "../../components/Dialog/Index";
import { useGetListPeopleQuery } from "../../store/services/Person";
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query";
import { SerializedError } from "@reduxjs/toolkit/dist/createAsyncThunk";
import { toast } from 'react-toastify';
import { Box } from "@mui/material";
import { IconButton } from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import DeleteIcon from "@mui/icons-material/Delete";
import CardButton from "../../components/cards/CardButton";

interface ListRelationshipProps {}
interface Item {
  key: string;
  label: string;
}
const ListPeople: React.FunctionComponent<ListRelationshipProps> = () => {
  const { personid, personName }: any = useParams();
  const { data: relation } = useGetRelationShipsByPersonIdQuery(personid);
  const { data: relationTypes } = useGetRelationShipTypesQuery();
  const { data: people } = useGetListPeopleQuery();
  const [createRelation, { isLoading: isUpdating }] = useCreateRelationshipMutation();
  const [openDialog, setOpenDialog] = useState(false);
  const [valueRol, setValueRol] = useState<Item | null>(null);
  const [valuePerson, setValuePerson] = useState<Item | null>(null);
  const [deletePerson] = useDeleteRelationshipMutation()

  const columnsDataGrid: GridColDef[] = [
    { field: "id", headerName: "ID", minWidth: 15, hide: true },
    { field: "relationshipTypeDescription", headerName: "Rol", minWidth: 170, flex:1  },
    { field: "firstName", headerName: "Nombre", minWidth: 170, flex:1  },
    { field: "lastName", headerName: "Apellido", minWidth: 170, flex:1  },
    {
      field: "action",
      headerName: "Action",
      minWidth: 160,
      sortable: false,
      filterable:false,
      flex:1,
      hideable:false,
      disableColumnMenu:true,
      renderCell: (params) => {
        const onClickDeletePerson = (id:string) => {
          var bool=window.confirm("Seguro de eliminar esta Relacion?")
          bool && deletePerson(id).then((response: { data: string; } | { error: FetchBaseQueryError | SerializedError; })=>{
              if("data" in response){
                  toast.success(`Relacion eliminada correctamente`);
              }
              if("error" in response){
                  toast.error("Error al momento de Eliminar la relacion");
              }
          });
      };
        return (
          <>
            <IconButton
              onClick={() => {
                onClickDeletePerson(params.row.id);
              }}
              color="secondary"
            >
              <DeleteIcon />
            </IconButton>
             </>
        );
      },
    },
  ];


  const handleClickOpen = () => {
    console.log("open dialog");
    setOpenDialog(true);
  };

  const handleClose = () => {
    console.log("close dialog");
    setOpenDialog(false);
  };

  const handleOkaction = () => {
    console.log("OK ACTION")
    const relation:IRelationShip={
        personId:personid,
        relation:{
            personID: valuePerson?.key as string,
            relationshipTypeID : valueRol?.key as string,
            isNeutral:false 
        }
    }
    createRelation(relation).then((response: { data: string } | { error: FetchBaseQueryError | SerializedError; }) => {
        if ("data" in response) {
            setOpenDialog(false);
            toast.success(`Relacion creada existosamente`);
        }
        if ("error" in response) {
            toast.error("Error al momento de crear la relacion");
        }
    });


  };

  return (
    <MainCard
      title={`Relaciones de ${personName}`}
      secondary={
        <>
        <CardButton type="back" title="Lista de Personas" link="/person" />
        <CardLink
          type="plus"
          title="Adicionar Relacion"
          action={handleClickOpen}
        />
      </>       
    }
    >
      <Box sx={{ height: 400, width: "100%" }}>
          <DataGrid
            rows={relation !== undefined ? (relation as IRelationshipByPerson[]) : []}
            columns={columnsDataGrid}
            pageSize={5}
            rowsPerPageOptions={[5]}
            disableSelectionOnClick
          />
        </Box>
      {openDialog && (
        <DialogForm
          openDialog={openDialog}
          title="Adicionar Relacion"
          okAction={handleOkaction}
          cancelAction={handleClose}
        >
          <div>
            <Box sx={{ mt:2}}>
            <Autocomplete
              value={valueRol}
              onChange={(event: any, newValue: Item | null) => {
                setValueRol(newValue);
                console.log(newValue?.key);
              }}
              id="autocompleteRol"
              options={relationTypes!.map((r) => ({
                key: r.id,
                label: r.relationshipName,
              }))}
              sx={{ width: 300 }}
              renderInput={(params) => <TextField {...params} label="Rol" />}
            />
            </Box>
            <Box sx={{ mt:2}}>
            <Autocomplete
              value={valuePerson}
              onChange={(event: any, newValue: Item | null) => {
                setValuePerson(newValue);
                console.log(newValue?.key);
              }}
              id="autocompletePerson"
              options={people!
                .map((p) => ({
                  key: p.id,
                  label: `${p.firstName} ${p.lastName}`,
                }))
                .filter((o) => o.key !== personid)}
              sx={{ width: 300 }}
              renderInput={(params) => (
                <TextField {...params} label="Persona" />
              )}
            />
            </Box>
            {
              (valuePerson && valueRol) ?
              <h6>{`${valuePerson?.label} es ${valueRol?.label} de ${personName}`}</h6>
              :null
            }
          </div>
        </DialogForm>
      )}
    </MainCard>
  );
};

export default ListPeople;
