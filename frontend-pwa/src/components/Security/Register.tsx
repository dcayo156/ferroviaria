import * as React from "react";
import { useState, useEffect } from 'react';
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import Link from '@mui/material/Link';
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import {NavLink, useNavigate} from 'react-router-dom'
import { styled } from '@mui/material/styles';
import type { IRegisterRequest,IRegisterResponse } from '../../store/types/Auth';
import { useRegisterMutation } from '../../store/services/Auth'
import { toast } from 'react-toastify';
import "./NavLink.css";
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query";
import { SerializedError } from "@reduxjs/toolkit";
import SubmitLoadingButton from "../Buttons/SubmitLoadingButton";
const AppBarDisplay = styled('div')(({ theme }) => ({
    [theme.breakpoints.down('md')]: { //cuando es chico
        display: 'none',
    },
}));
function Copyright(props: any) {
    return (
        <Typography variant="body2" color="text.secondary" align="center" {...props}>
            {'Copyright © '}
            <Link color="inherit" href="#">
                La Juana
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}
export default function Register() {
    const navigate =  useNavigate();
    const [personRegister, setPersonRegister] = useState<IRegisterRequest>({
        nombre: '',
        apellidos: '',
        email: '',
        username:'',
        password: '',
        confirmpassword: ''
    });
    const [registerUser] = useRegisterMutation();
    const [isLoading,setIsLoading]=React.useState(false);
    const [errorPassword,setErrorPassword] = useState<boolean>(false);
    const [errorConfirmPassword,setErrorConfirmPassword] = useState<boolean>(false);
    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        //TODO Valdiate personRegister
        if(personRegister.confirmpassword===''){
            toast.warning("Confirme la Contraseña")
            return;
        }
        setIsLoading(true)
        
        registerUser(personRegister).then((response: { data: IRegisterResponse; } | { error: FetchBaseQueryError | SerializedError; })=>{
            if("data" in response){
                toast.success("Registro exitoso");
                navigate("/authentication/login");
            }
            if("error" in response){
                if("message" in response.error)
                    toast.error(response.error.message);
                if("error" in response.error)
                    toast.error(response.error.error);
            }
            setIsLoading(false)
        });
    };
    const handleChanges = (e:any) => {
        const { name, value } = e.target;
        setPersonRegister({
            ...personRegister,
            [name]: value
        })
    }
    useEffect(()=>{
        personRegister.password===personRegister.confirmpassword?
        setErrorConfirmPassword(false):setErrorConfirmPassword(true);
    },[personRegister.confirmpassword])
    
    const validatePassword = () =>{
        let regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&#()._-])[A-Za-z\d$@$!%*?&#()._-]{8,15}$/;
        if(!personRegister.password.match(regex)){
            setErrorPassword(true);
        }
        else{
            setErrorPassword(false);
        }
    }

    return (

        <Container sx={{pb:3}}
            component="main" maxWidth="xs" >
            <CssBaseline />
            <Box
                sx={{
                    marginTop: 8,
                    display: "flex",
                    flexDirection: "column",
                    alignItems: "center",
                }}
            >
                <AppBarDisplay>
                    <Avatar sx={{ m: 1, bgcolor: "action", width: 56, height: 56 }}>
                        <AccountCircleIcon sx={{ fontSize: 50 }} />
                    </Avatar>
                </AppBarDisplay>
                <Typography component="h1" variant="h5">
                    Registro de Usuario
                </Typography>
                <Box
                    component="form"
                    onSubmit={handleSubmit}
                    noValidate
                    sx={{ mt: 1 }}
                >
                    <Grid container>
                        <Grid item xs >
                            <TextField
                                margin="normal"
                                required
                                fullWidth
                                id="nombre"
                                label="Nombre"
                                name="nombre"
                                autoFocus
                                onChange={handleChanges}
                            />
                        </Grid>
                        <Grid item >
                            <TextField
                                margin="normal"
                                required
                                fullWidth
                                id="apellidos"
                                label="Apellido/s"
                                name="apellidos"                                
                                onChange={handleChanges}
                            />
                        </Grid>
                    </Grid>

                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="email"
                        label="Email"
                        name="email"
                        autoComplete="email"
                        onChange={handleChanges}
                    />
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="username"
                        label="Username"
                        name="username"
                        onChange={handleChanges}
                    />
                    <TextField
                        error={errorPassword}
                        margin="normal"
                        required
                        fullWidth
                        name="password"
                        label="Contraseña"
                        type="password"
                        id="password"
                        onBlur={validatePassword}
                        onChange={handleChanges}
                        autoComplete="current-password"
                        helperText={errorPassword?`Minimo 8 caracteres,
                        Maximo 15,
                        Al menos una letra mayúscula,
                        Al menos una letra minúscula,
                        Al menos un dígito,
                        No espacios en blanco,
                        Al menos 1 caracter especial`:""}
                    />
                    <TextField
                        error={errorConfirmPassword}
                        margin="normal"
                        required
                        fullWidth
                        name="confirmpassword"
                        label="Repetir Contraseña"
                        type="password"
                        id="confirmpassword"
                        onChange={handleChanges}
                        autoComplete="current-password"
                        helperText={errorConfirmPassword?"El password no coincide":""}
                    />
                    <SubmitLoadingButton loading={isLoading} text="Registrar" fullWidth={true} />
                    <Grid container >
                        <Grid item  >
                          
                            <NavLink to="/authentication/login" className="NavLink-react-router" > 
                            ¿Ya tienes cuenta ?    Logueate
                            </NavLink> 
                        </Grid>

                    </Grid>
                </Box>
            </Box>
            <Copyright sx={{ mt: 8, mb: 4 }} />
        </Container>

    );
}