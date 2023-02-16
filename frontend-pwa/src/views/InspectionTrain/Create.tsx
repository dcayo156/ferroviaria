import * as React from "react";
import { useState } from 'react';
import Grid from "@mui/material/Grid";
import { useNavigate} from 'react-router-dom'
import { useCreateInspectionTrainsMutation } from '../../store/services/InspectionTrain'
import { toast } from 'react-toastify';
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query";
import { SerializedError } from "@reduxjs/toolkit";
import MainCard from '../../components/cards/MainCard';
import CardButton from '../../components/cards/CardButton';
import FormInspectionTrain from "./Forms/FormDocument2";
import SaveIcon from '@mui/icons-material/Save';
import { IInspectionTrainCreate } from '../../store/types/InspectionTrain';
import LoadingButton from "../../components/Buttons/LoadingButton";
export default function Register() {
    const navigate =  useNavigate();
    const [InspectionTrain, setInspectionTrain] = useState<IInspectionTrainCreate>({
        id:"",
        codigo:"",
        fileName: "",
        categoryId: "",
        subCategoryId: "",
        filePath:""
    });
    const [registerInspectionTrain] = useCreateInspectionTrainsMutation();
    const [isLoading,setIsLoading]=React.useState(false);
    const isURL=(str:string)=> {
        var pattern = new RegExp('^(https?:\\/\\/)?'+ // protocol
        '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.?)+[a-z]{2,}|'+ // domain name
        '((\\d{1,3}\\.){3}\\d{1,3}))'+ // OR ip (v4) address
        '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*'+ // port and path
        '(\\?[;&a-z\\d%_.~+=-]*)?'+ // query string
        '(\\#[-a-z\\d_]*)?$','i'); // fragment locator
        return pattern.test(str);
      }
    const saveChanges = () => { 
        setIsLoading(true)
        //console.log(InspectionTrain)
        const useToCreate: IInspectionTrainCreate = {
            id:"",
            codigo:InspectionTrain.codigo,
            fileName: InspectionTrain.fileName,
            categoryId: InspectionTrain.categoryId,
            subCategoryId: InspectionTrain.subCategoryId,
            filePath: InspectionTrain.filePath
          };
          if(useToCreate.categoryId=="" || useToCreate.subCategoryId==""){
            toast.warning("Ingrese todos los datos");
            setIsLoading(false)
            return
          } 
            
          registerInspectionTrain(useToCreate).then((response: { data: string } | { error: FetchBaseQueryError | SerializedError; })=>{
            if("data" in response){
                toast.success("Registro exitoso");
                navigate("/InspectionTrain");
            }
            if("error" in response){
                if("message" in response.error)
                    toast.error(response.error.message);
                if("error" in response.error)
                    toast.error(response.error.error);
            }
            setIsLoading(false)
        });
    }
    return (
    <MainCard 
        title="Registrar inspección" 
        secondary={
            <CardButton type="back" title="Lista de inspecciones integrales de trenes químicos" link="/InspectionTrain" />}
    >{
        <>
        <FormInspectionTrain inspectionTrain={InspectionTrain} setInspectionTrain={setInspectionTrain} isCreate= {true}/>
        <Grid item xs={12} md={12} display="flex" justifyContent="center">
            <LoadingButton
                loading={isLoading}
                text="Guardar todos los cambios"
                onClick={saveChanges}
                startIcon={<SaveIcon />}/>
        </Grid>                    
    </>   
    }
    </MainCard>);
}