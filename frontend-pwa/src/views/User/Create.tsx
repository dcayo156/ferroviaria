import * as React from "react";
import { useState } from 'react';
import Grid from "@mui/material/Grid";
import { useNavigate} from 'react-router-dom'
import type { IUserRequest,IRegisterResponse } from '../../store/types/Auth';
import { useRegisterMutation } from '../../store/services/Auth'
import { toast } from 'react-toastify';
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query";
import { SerializedError } from "@reduxjs/toolkit";
import MainCard from '../../components/cards/MainCard';
import CardButton from '../../components/cards/CardButton';
import UserData from "./Forms/UserData";
import SaveIcon from '@mui/icons-material/Save';
import LoadingButton from "../../components/Buttons/LoadingButton";
export default function Register() {
    const navigate =  useNavigate();
    const [userRegister, setUserRegister] = useState<IUserRequest| Partial<IUserRequest>>({
        id:'',
        nombre: '',
        apellidos: '',
        email: '',
        username:'',
        password: '',
        confirmpassword: ''
    });
    const [registerUser] = useRegisterMutation();
    const [isLoading,setIsLoading]=React.useState(false);

    const saveChanges = () => { 
        if(userRegister.confirmpassword===''){
            toast.warning("Confirme la Contraseña")
            return;
        }
        setIsLoading(true)
        const userToCreate: IUserRequest = {
            id:'',
            nombre: userRegister.nombre!,
            apellidos: userRegister.apellidos!,
            email: userRegister.email!,
            username:userRegister.username!,
            password: userRegister.password!,
            confirmpassword: userRegister.confirmpassword!
          };
        registerUser(userToCreate).then((response: { data: IRegisterResponse; } | { error: FetchBaseQueryError | SerializedError; })=>{
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
        });
    }
    return (
    <MainCard 
        title="Registrar usuario" 
        secondary={
            <CardButton type="back" title="Lista de Usuarios" link="/user" />}
    >{
        <>
        <UserData user={userRegister} setUser={setUserRegister} isCreate= {true}/>
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