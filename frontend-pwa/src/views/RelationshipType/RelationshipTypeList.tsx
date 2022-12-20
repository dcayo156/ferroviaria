import TextField from "@mui/material/TextField";
import { useState } from "react";
import MainCard from "../../components/cards/MainCard";
import {
  useCreateRelationshipTypeMutation,
  useDeleteRelationshipTypeMutation,
  useGetRelationshipTypesGroupedQuery,
  useUpdateRelationshipTypeMutation,
} from "../../store/services/Relationship";
import CardLink from "../../components/cards/CardLink";
import DialogForm from "../../components/Dialog/Index";
import { toast } from "react-toastify";
import {
  ICreateRelationshipType,
  IRelationshipType,
  IRelationshipTypesGrouped,
} from "../../store/types/delete-RelationshipType";
import { Box, Checkbox, FormControlLabel, FormGroup } from "@mui/material";
import { IconButton } from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query";
import { SerializedError } from "@reduxjs/toolkit";

interface Props {}

const RelationshipTypeList: React.FunctionComponent<Props> = () => {
  const { data: relationsType } = useGetRelationshipTypesGroupedQuery();
  const [createRelationshipType, { isLoading: isUpdating }] =
    useCreateRelationshipTypeMutation();
  const [updateRelationshipType] = useUpdateRelationshipTypeMutation();
  const [deleteRelationshipType] = useDeleteRelationshipTypeMutation();
  const [openDialog, setOpenDialog] = useState(false);
  const [isUpdated, setIsUpdated] = useState(false);
  const [isOneRelationType, setIsOneRelationType] = useState(false);
  
  const [relationType1, setRelationType1] = useState<IRelationshipType>({
    id: null,
    relationshipTypeRequiredID: null,
    femaleDescription: "",
    maleDescription: "",
    neutralDescription: "",
  });
  const [relationType2, setRelationType2] = useState<IRelationshipType>({
    id: null,
    relationshipTypeRequiredID: null,
    femaleDescription: "",
    maleDescription: "",
    neutralDescription: "",
  });

  const columnsDataGrid: GridColDef[] = [
    { field: "id", headerName: "id", minWidth: 15, hide: true },
    {
      field: "idRelationType1",
      headerName: "idRelationType1",
      minWidth: 15,
      hide: true,
    },
    {
      field: "idRelationType2",
      headerName: "idRelationType1",
      minWidth: 15,
      hide: true,
    },
    {
      field: "relationShipName1",
      type: "string",
      flex: 1,
      headerName: "Relacion 1",
      minWidth: 400,
    },
    {
      field: "relationShipName2",
      type: "string",
      flex: 1,
      headerName: "Relacion 2",
      minWidth: 400,
    },
    {
      field: "action",
      headerName: "Action",
      minWidth: 160,
      flex: 1,
      sortable: false,
      filterable: false,
      hideable: false,
      disableColumnMenu: true,
      renderCell: (params) => {
        const onClickDeleteRelation = (id: string) => {
          var bool=window.confirm("Seguro de eliminar este Tipo de Relacion?");
          bool && deleteRelationshipType(params.row.idRelationType1).then((response: { data: string; } | { error: FetchBaseQueryError | SerializedError; })=>{
              if("data" in response){
                  toast.success(`Tipo de Relacion eliminada correctamente`);
              }
              if("error" in response){
                  toast.error("Error al momento de Eliminar el tipo de relacion");
              }
          });
        };
        const onClickEditRelation = (id: string) => {
          setIsUpdated(true);
          let relationType1data: string[] = params.row.relationShipName1.split("/",3);
          let relationType2data: string[] = params.row.relationShipName2.split("/",3);

          let relationType1: IRelationshipType = {
            id: params.row.idRelationType1,
            relationshipTypeRequiredID: params.row.idRelationType2,
            maleDescription: relationType1data[0],
            femaleDescription: relationType1data[1],
            neutralDescription: relationType1data[2],
          };
          setRelationType1(relationType1);
          if (params.row.idRelationType1 !== params.row.idRelationType2) {
            setIsOneRelationType(false);
            let relationType2: IRelationshipType = {
              id: params.row.idRelationType2,
              relationshipTypeRequiredID: params.row.idRelationType1,
              maleDescription: relationType2data[0],
              femaleDescription: relationType2data[1],
              neutralDescription: relationType2data[2],
            };
            setRelationType2(relationType2);
          } else {
            setIsOneRelationType(true);
            setRelationType2({
              id: null,
              relationshipTypeRequiredID: null,
              femaleDescription: "",
              maleDescription: "",
              neutralDescription: "",
            });
          }
          setOpenDialog(true);
        };
        return (
          <>
            <IconButton
              onClick={() => {
                onClickEditRelation(params.row.id);
              }}
              color="secondary"
            >
              <EditIcon />
            </IconButton>
            <IconButton
              onClick={() => {
                onClickDeleteRelation(params.row.id);
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
    setIsUpdated(false);
    setIsOneRelationType(false);
    setRelationType1({
      id: null,
      relationshipTypeRequiredID: null,
      femaleDescription: "",
      maleDescription: "",
      neutralDescription: "",
    });
    setRelationType2({
      id: null,
      relationshipTypeRequiredID: null,
      femaleDescription: "",
      maleDescription: "",
      neutralDescription: "",
    });
    setOpenDialog(true);
  };

  const handleClose = () => {
    setOpenDialog(false);
  };
  const handleChange = () => {
    setIsOneRelationType(!isOneRelationType);
  };

  const handleCreateRelationshipType = () => {
    let relation: ICreateRelationshipType;
    if (isOneRelationType) {
      relation = {
        items: [
          {
            id: null,
            relationshipTypeRequiredID: null,
            femaleDescription: relationType1.femaleDescription,
            maleDescription: relationType1.maleDescription,
            neutralDescription: relationType1.neutralDescription,
          },
        ],
      };
    } else {
      relation = {
        items: [
          {
            id: null,
            relationshipTypeRequiredID: null,
            femaleDescription: relationType1.femaleDescription,
            maleDescription: relationType1.maleDescription,
            neutralDescription: relationType1.neutralDescription,
          },
          {
            id: null,
            relationshipTypeRequiredID: null,
            femaleDescription: relationType2.femaleDescription,
            maleDescription: relationType2.maleDescription,
            neutralDescription: relationType2.neutralDescription,
          },
        ],
      };
    }

    createRelationshipType(relation).then(
      (
        response:
          | { data: string }
          | { error: FetchBaseQueryError | SerializedError }
      ) => {
        if ("data" in response) {
          setOpenDialog(false);
          toast.success(`Tipo de Relacion creada existosamente`);
        }
        if ("error" in response) {
          toast.error("Error al momento de crear el tipo de relacion");
        }
      }
    );
  };
  const handleUpdateRelationshipType = () => {
    updateRelationshipType(relationType1).then(
      (
        response:
          | { data: string }
          | { error: FetchBaseQueryError | SerializedError }
      ) => {
        if ("data" in response) {
          toast.success(`Tipo de Relacion actualizada existosamente`);
        }
        if ("error" in response) {
          toast.error("Error al momento de actualizar el tipo de relacion");
        }
      }
    );
    if (!isOneRelationType) {
      updateRelationshipType(relationType2).then(
        (
          response:
            | { data: string }
            | { error: FetchBaseQueryError | SerializedError }
        ) => {
          if ("data" in response) {
          }
          if ("error" in response) {
          }
        }
      );
    }
    setOpenDialog(false);
  };

  const handleOkaction = () => {
    if (!isUpdated) {
      handleCreateRelationshipType();
    } else {
      handleUpdateRelationshipType();
    }
  };

  const handleDataChangeRelation1 = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRelationType1({
      ...relationType1,
      [event.target.name]: event.target.value,
    });
  };

  const handleDataChangeRelation2 = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRelationType2({
      ...relationType2,
      [event.target.name]: event.target.value,
    });
  };
  return (
    <MainCard
      title={`Tipos de Relaciones`}
      secondary={
        <CardLink
          type="plus"
          title="Adicionar Tipo de Relacion"
          action={handleClickOpen}
        />
      }
    >
      <Box sx={{ height: 500, width: "100%" }}>
        <DataGrid
          rows={
            relationsType !== undefined
              ? (relationsType as IRelationshipTypesGrouped[])
              : []
          }
          columns={columnsDataGrid}
          pageSize={10}
          rowsPerPageOptions={[10]}
          disableSelectionOnClick
        />
      </Box>
      {openDialog && (
        <DialogForm
          openDialog={openDialog}
          title={
            !isUpdated ? "Adicionar Tipo Relacion" : "Modificar Tipo Relacion"
          }
          okAction={handleOkaction}
          cancelAction={handleClose}
        >
          <div>
            {!isUpdated ? (
              <Box sx={{ mt: 2 }}>
                <FormGroup>
                  <FormControlLabel
                    control={
                      <Checkbox
                        checked={isOneRelationType}
                        onChange={handleChange}
                      />
                    }
                    label="Tipo de relacion asociada consigo misma"
                  />
                </FormGroup>
              </Box>
            ) : null}
            <Box sx={{ mt: 2 }}>
              <h4>Descripcion Masculina:</h4>
              <TextField
                id="maleDescription1"
                name="maleDescription"
                value={relationType1.maleDescription}
                label="Relacion 1"
                variant="outlined"
                size="small"
                onChange={handleDataChangeRelation1}
              />
              <TextField
                id="maleDescription1"
                name="maleDescription"
                label="Relacion 2"
                variant="outlined"
                size="small"
                value={relationType2.maleDescription}
                disabled={isOneRelationType}
                onChange={handleDataChangeRelation2}
              />
            </Box>
            <Box sx={{ mt: 2 }}>
              <h4>Descripcion Femenima:</h4>
              <TextField
                id="femaleDescription1"
                name="femaleDescription"
                value={relationType1.femaleDescription}
                label="Relacion 1"
                variant="outlined"
                size="small"
                onChange={handleDataChangeRelation1}
              />
              <TextField
                id="femaleDescription2"
                name="femaleDescription"
                value={relationType2.femaleDescription}
                label="Relacion 2"
                variant="outlined"
                size="small"
                disabled={isOneRelationType}
                onChange={handleDataChangeRelation2}
              />
            </Box>
            <Box sx={{ mt: 2 }}>
              <h4>Descripcion Neutral:</h4>
              <TextField
                id="neutralDescription1"
                name="neutralDescription"
                value={relationType1.neutralDescription}
                label="Relacion 1"
                variant="outlined"
                size="small"
                onChange={handleDataChangeRelation1}
              />
              <TextField
                id="neutralDescription2"
                name="neutralDescription"
                value={relationType2.neutralDescription}
                label="Relacion 2"
                variant="outlined"
                size="small"
                disabled={isOneRelationType}
                onChange={handleDataChangeRelation2}
              />
            </Box>
          </div>
        </DialogForm>
      )}
    </MainCard>
  );
};

export default RelationshipTypeList;
