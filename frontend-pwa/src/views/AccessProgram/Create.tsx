import * as React from "react";
import { useState } from 'react';
import Grid from "@mui/material/Grid";
import { useNavigate} from 'react-router-dom'
import { useRegisterMutation } from '../../store/services/Auth'
import { toast } from 'react-toastify';
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query";
import { SerializedError } from "@reduxjs/toolkit";
import MainCard from '../../components/cards/MainCard';
import CardButton from '../../components/cards/CardButton';
import FormAccessProgram from "./Forms/FormAccessProgram";
import SaveIcon from '@mui/icons-material/Save';
import { IAccessProgramRequest } from '../../store/types/AccessProgram';
import LoadingButton from "../../components/Buttons/LoadingButton";
export default function Register() {
    const navigate =  useNavigate();
    const [accessProgram, setAccessProgram] = useState<IAccessProgramRequest>({
        id:'',
        fileName:"",
        iconName:"",
        name:"",
        url:""
    });
    const [registerUser] = useRegisterMutation();
    const [isLoading,setIsLoading]=React.useState(false);

    const saveChanges = () => { 
        setIsLoading(true)
        const userToCreate: IAccessProgramRequest = {
            id:'',
            iconName:accessProgram.iconName,
            fileName:accessProgram.fileName,
            name:accessProgram.name,
            url:accessProgram.url
          };
        /*registerUser(userToCreate).then((response: { data: IRegisterResponse; } | { error: FetchBaseQueryError | SerializedError; })=>{
            if("data" in response){
                toast.success("Registro exitoso");
                navigate("/user");
            }
            if("error" in response){
                if("message" in response.error)
                    toast.error(response.error.message);
                if("error" in response.error)
                    toast.error(response.error.error);
            }
            setIsLoading(false)
        });*/
    }
    return (
    <MainCard 
        title="Registrar Acceso de Programa" 
        secondary={
            <CardButton type="back" title="Lista de Usuarios" link="/user" />}
    >{
        <>
        <FormAccessProgram accessprogram={accessProgram} setAccessProgram={setAccessProgram} isCreate= {true}/>
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