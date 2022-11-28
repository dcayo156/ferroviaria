import { Grid, LinearProgress, TextField } from '@mui/material';
import * as React from 'react';
import CardButton from '../../components/cards/CardButton';
import FormCard from '../../components/cards/FormCard';
import MainCard from '../../components/cards/MainCard';
import LockIcon from '@mui/icons-material/Lock'
import SaveIcon from '@mui/icons-material/Save'
import { getCredentials } from '../../store/slices/Auth/localStorage';
import { useUpdatePasswordMutation } from '../../store/services/Auth'
import LoadingButton from '../../components/Buttons/LoadingButton';
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';
import { toast } from 'react-toastify';
import { IRegisterResponse } from '../../store/types/Auth'
import { hasError } from '../../components/Security/ErrorManager';
import { useNavigate, useParams } from 'react-router-dom';

interface ChangePasswordProps {

}
interface FormPassword {
    currentPassword: string
    newPassword: string
    id: string
}
const ChangePassword: React.FunctionComponent<ChangePasswordProps> = () => {
    const { id }: any = useParams();
    const [
        updatedPassword,
        { isLoading: isUpdating },
    ] = useUpdatePasswordMutation()
    //const auth = getCredentials();
    const [formPassword, setFormPassword] = React.useState<FormPassword>({ currentPassword: "", newPassword: "", id: id as string })
    const [errorConfirmPassword, setErrorConfirmPassword] = React.useState<boolean>(true);
    const [errorPassword,setErrorPassword] = React.useState<boolean>(false);
    const [confirmPassword, setConfirmPassword] = React.useState<string>("");
    const navigate=useNavigate();
    const saveChanges=()=>{
        updatedPassword(formPassword).then((response: { data: IRegisterResponse } | { error: FetchBaseQueryError | SerializedError; }) => {
            if(hasError(response,"Error al momento de crear la persona")){
                return;
            }
            if ("data" in response) {
                setFormPassword({ currentPassword: "", newPassword: "", id: id as string });
                setConfirmPassword("");
                toast.success('Contraseña cambiada exitosamente');
                navigate(`/auth/show/user-authenticated`);
            }
        });
    }
    React.useEffect(()=>{
        formPassword.newPassword===confirmPassword?
        setErrorConfirmPassword(false):setErrorConfirmPassword(true);
    },[formPassword.newPassword,confirmPassword])
    
    const validatePassword = () =>{
        let regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&#()._-])[A-Za-z\d$@$!%*?&#()._-]{8,15}$/;
        if(!formPassword.newPassword.match(regex)){
            setErrorPassword(true);
        }
        else{
            setErrorPassword(false);
        }
    }
    return (<MainCard title="Cambiar Contraseña" secondary={<CardButton type="back" title="cancelar" link="/auth/show/user-authenticated" />} >

        <FormCard
            icon={<LockIcon />}
            style={undefined}
            title='Cambiar la Contraseña'
            key='change password'
            action={null}>
                
            <Grid container spacing={2}>
                <Grid item xs={12} lg={4}>
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="currentPassword"
                        label="Contraseña Actual"
                        name="currentPassword"
                        type="password"
                        value={formPassword!.currentPassword}
                        onChange={({ target: { value } }) => setFormPassword({ ...formPassword, "currentPassword": value })}
                        autoFocus
                        autoComplete="false"
                    />
                </Grid>
                <Grid item xs={12} lg={4}>
                    <TextField
                        error={errorPassword}
                        margin="normal"
                        required
                        fullWidth
                        name="newPassword"
                        label="Nueva Contraseña"
                        type="password"
                        id="newPassword"
                        onBlur={validatePassword}
                        value={formPassword!.newPassword}
                        onChange={({ target: { value } }) => setFormPassword({ ...formPassword, "newPassword": value })}
                        autoComplete="false"
                        helperText={errorPassword?`Minimo 8 caracteres,
                        Maximo 15,
                        Al menos una letra mayúscula,
                        Al menos una letra minúscula,
                        Al menos un dígito,
                        No espacios en blanco,
                        Al menos 1 caracter especial`:""}
                    />
                </Grid>
                <Grid item xs={12} lg={4}>
                    <TextField
                        error={errorConfirmPassword}
                        margin="normal"
                        required
                        fullWidth
                        name="confirmPassword"
                        label="Confirme la Contraseña"
                        type="password"
                        id="confirmPassword"
                        value={confirmPassword}
                        onChange={({ target: { value } }) => setConfirmPassword(value)}
                        autoComplete="false"
                        helperText={errorConfirmPassword?"El password no coincide":""}
                    />
                </Grid>
                <Grid item xs={12} md={12} display="flex" justifyContent="center">
                    <LoadingButton
                        loading={isUpdating}
                        text="Guardar los cambios"
                        onClick={saveChanges}
                        startIcon={<SaveIcon />}/>
                </Grid>
            </Grid>
        </FormCard>
    </MainCard>);
}

export default ChangePassword;