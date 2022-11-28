import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import PersonIcon from '@mui/icons-material/Person';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { styled } from '@mui/system';
import { NavLink } from 'react-router-dom'
import { useNavigate } from 'react-router-dom';
import "./NavLink.css";
import { LoginRequest, LoginResponse } from '../../store/types/Auth';
import { useLoginMutation } from '../../store/services/Auth';
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';
import { toast } from 'react-toastify';
import { useDispatch } from 'react-redux'
import { setCredentials } from '../../store/slices/Auth';
import { remember, forget, getRemember } from '../../store/slices/Auth/localStorage'
import SubmitLoadingButton from '../Buttons/SubmitLoadingButton'

const AvatarDisplay = styled('div')(({ theme }) => ({
    [theme.breakpoints.down('md')]: {
        display: 'none',
    },
}));
const ErrorComponent = styled('div')({
    color: 'red',
    padding: 4,
});

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
export default function Login() {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const credentialFromStorage=getRemember();
    const [credentialData,setCredentialData]= React.useState<LoginRequest>({
        email:credentialFromStorage.email,
        password:credentialFromStorage.password
    })
    const [login] = useLoginMutation();
    const [isLoading,setIsLoading]=React.useState(false);
    const [error, setError] = React.useState(false);
    const [checked, setChecked] = React.useState(credentialFromStorage.remember);
    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        setIsLoading(true)
        event.preventDefault();
        login(credentialData).then((response: { data: LoginResponse; } | { error: FetchBaseQueryError | SerializedError; })=>{
            if("data" in response){
                setError(false);
                dispatch(setCredentials(response.data));
                navigate('/main/Home');
            }
            if("error" in response){
                setError(true);
                toast.error("credenciales invalidas");
            }
            setIsLoading(false)
        });
        if(checked){
            remember({email:credentialData.email,password:credentialData.password,remember:checked});
        }else{
            forget();
        }
    };
    const handleChanges = (e:any) => {
        const { name, value } = e.target;
        setCredentialData({
            ...credentialData,
            [name]: value
        })
    }


    return (
        <Container sx={{pb:3}} component="main" maxWidth="xs">
            <CssBaseline />
            <Box
                sx={{
                    marginTop: 8,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}
            >
                <AvatarDisplay>
                    <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }} style={{ fontSize: "160px" }}>
                        <PersonIcon />
                    </Avatar>
                </AvatarDisplay>
                <Typography component="h1" variant="h5">
                    Ingrese su usuario
                </Typography>
                <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        value={credentialData.email}
                        id="email"
                        label="Email"
                        name="email"
                        autoComplete="email"
                        onChange={handleChanges}
                        autoFocus
                    />
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        value={credentialData.password}
                        name="password"
                        label="Contraseña"
                        type="password"
                        id="password"
                        autoComplete="current-password"
                        onChange={handleChanges}
                    />
                    <FormControlLabel
                        control={<Checkbox checked={checked} onChange={()=>setChecked(!checked)} value="remember" color="primary" />}
                        label="Recordar usuario"
                    />
                    {
                        error
                            ?
                            <ErrorComponent>
                                Email o Contraseña Incorrecta
                            </ErrorComponent>
                            :
                            ''
                    }
                    <SubmitLoadingButton loading={isLoading} text="Ingresar" fullWidth={true} />
                    <Grid container>
                        <Grid item xs>
                            <NavLink to="/authentication/register" className="NavLink-react-router">
                                ¿No tienes cuenta? Registrate
                            </NavLink>
                        </Grid>
                        <Grid item >
                            <NavLink to="#" className="NavLink-react-router" >
                                ¿Has olvidado tu contraseña?
                            </NavLink>
                        </Grid>
                    </Grid>
                </Box>
            </Box>
            <Copyright sx={{ mt: 8, mb: 4 }} />
        </Container >

    );
}