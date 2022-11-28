import * as React from 'react';
import FormCard from '../../../components/cards/FormCard';
import { IUserRequest } from '../../../store/types/Auth';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { Box, Container, CssBaseline, Grid,  TextField } from '@mui/material';
interface UserDataProps {
    user: IUserRequest | Partial<IUserRequest>
    setUser: (value: React.SetStateAction<IUserRequest | Partial<IUserRequest>>) => void
    isCreate:boolean
}
const UserData: React.FunctionComponent<UserDataProps> = ({ user, setUser, isCreate }) => {
    const [errorPassword,setErrorPassword] = React.useState<boolean>(false);
    const [errorConfirmPassword,setErrorConfirmPassword] = React.useState<boolean>(false);
    const validatePassword = () =>{
        let regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&#()._-])[A-Za-z\d$@$!%*?&#()._-]{8,15}$/;
        if(!user.password!.match(regex)){
            setErrorPassword(true);
        }
        else{
            setErrorPassword(false);
        }
    }
    
    React.useEffect(()=>{
        user.password===user.confirmpassword?
        setErrorConfirmPassword(false):setErrorConfirmPassword(true);
    },[user.confirmpassword])

  return  <FormCard
    icon={<AccountCircleIcon sx={{ fontSize: 50 }} />}
    style={undefined}
    title='Registrar Datos de Usuario'
    key='my information'
    action={null}>
    <Container sx={{pb:3}}
        component="main" maxWidth="xs" >
        <CssBaseline />
        <Box
            sx={{
                marginTop: 2,
                display: "flex",
                flexDirection: "column",
                alignItems: "center",
            }}
        >            
            <Box
                component="form"
                //onSubmit={handleSubmit}
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
                            value={user!.nombre}
                            onChange={({ target: { value } }) => setUser({ ...user, "nombre": value })}
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
                            value={user!.apellidos}
                            onChange={({ target: { value } }) => setUser({ ...user, "apellidos": value })}
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
                    value={user!.email}
                    onChange={({ target: { value } }) => setUser({ ...user, "email": value })}
                />
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    id="username"
                    label="Username"
                    name="username"
                    value={user!.username}
                    onChange={({ target: { value } }) => setUser({ ...user, "username": value })}
                />                
                {!isCreate ? (<br/>):(
                     <><TextField
                          error={errorPassword}
                          margin="normal"
                          required
                          fullWidth
                          name="password"
                          label="Contraseña"
                          type="password"
                          id="password"
                          onBlur={validatePassword}
                          value={user!.password}
                          onChange={({ target: { value } }) => setUser({ ...user, "password": value })}
                          autoComplete="current-password"
                          helperText={errorPassword ? `Minimo 8 caracteres,
                     Maximo 15,
                     Al menos una letra mayúscula,
                     Al menos una letra minúscula,
                     Al menos un dígito,
                     No espacios en blanco,
                     Al menos 1 caracter especial` : ""} /><TextField
                              error={errorConfirmPassword}
                              margin="normal"
                              required
                              fullWidth
                              name="confirmpassword"
                              label="Repetir Contraseña"
                              type="password"
                              id="confirmpassword"
                              value={user!.confirmpassword}
                              onChange={({ target: { value } }) => setUser({ ...user, "confirmpassword": value })}
                              autoComplete="current-password"
                              helperText={errorConfirmPassword ? "El password no coincide" : ""} /></>
                )}
                              
            </Box>
        </Box>          
    </Container>
</FormCard>
}
export default UserData