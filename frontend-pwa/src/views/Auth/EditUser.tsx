import { Box, TextField, Typography, Container, LinearProgress } from '@mui/material';
import * as React from 'react';
import CardButton from '../../components/cards/CardButton';
import FormCard from '../../components/cards/FormCard';
import MainCard from '../../components/cards/MainCard';
import LockIcon from '@mui/icons-material/Lock'
import SaveIcon from '@mui/icons-material/Save'
import { useGetUserByIDQuery, useUpdateRegisterMutation} from '../../store/services/Auth'
import LoadingButton from '../../components/Buttons/LoadingButton';
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';
import { toast } from 'react-toastify';
import { hasError } from '../../components/Security/ErrorManager';
import { IUserResponse, IRegisterResponse, LoginResponse } from '../../store/types/Auth'
import { useNavigate, useParams } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { setCredentials } from '../../store/slices/Auth';
import { getCredentials } from '../../store/slices/Auth/localStorage';

interface EditUserProps {

}

const EditUser: React.FunctionComponent<EditUserProps> = () => {
    const { id }: any = useParams();
    const [user, setUser] = React.useState<IUserResponse>({
        id: id,
        nombre: "",
        apellidos: "",
        email: "",
        username: ""
    });
    const { data, error, isLoading } = useGetUserByIDQuery(id as string,{
        skip: id==null,  
    });
    React.useEffect(()=>{
        data && setUser(data)
    },[data])
    const credential = getCredentials();
    const [
        updateRegister,
        { isLoading: isUpdating },
    ] = useUpdateRegisterMutation()
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const saveChanges = () => {
        updateRegister(user).then((response: { data: IRegisterResponse } | { error: FetchBaseQueryError | SerializedError; }) => {
            if (hasError(response, "Error al momento de crear la persona")) {
                return;
            }
            if ("data" in response) {
                toast.success(`Tus Datos han sido actualizados correctamente`);
                const updateData: LoginResponse = {
                    email: response.data.email,
                    username: response.data.username,
                    token: response.data.token,
                    refreshToken: credential.refreshToken,
                    id: credential.id
                }
                dispatch(setCredentials(updateData));
                navigate(`/auth/show/user-authenticated`)
            }
        });
    }
    return (
        <MainCard title="Editar Información" secondary={<CardButton type="back" title="Información de Usuario" link="/auth/show/user-authenticated" />} >
            {
                false ?
                    <LinearProgress color="secondary" />
                    :
                    <FormCard
                        icon={<LockIcon />}
                        style={undefined}
                        title='Cambiar la Contraseña'
                        key='change password'
                        action={null}>
                        <Container sx={{ pb: 3 }}
                            component="main" maxWidth="xs" >
                            <Box
                                sx={{
                                    marginTop: 1,
                                    display: "flex",
                                    flexDirection: "column",
                                    alignItems: "center",
                                }}
                            >
                                <Typography component="h1" variant="h5">
                                    Datos del Usuario
                                </Typography>
                                <Box
                                    component="form"
                                    onSubmit={() => { }}
                                    noValidate
                                    sx={{ mt: 1 }}
                                >
                                    <TextField
                                        margin="normal"
                                        size='small'
                                        required
                                        fullWidth
                                        id="nombre"
                                        label="Nombre"
                                        name="nombre"
                                        value={user.nombre}
                                        autoFocus
                                        onChange={({ target: { value } }) => { setUser({ ...user, nombre: value }) }}
                                    />
                                    <TextField
                                        margin="normal"
                                        size='small'
                                        required
                                        fullWidth
                                        id="apellidos"
                                        label="Apellido/s"
                                        name="apellidos"
                                        value={user.apellidos}
                                        onChange={({ target: { value } }) => { setUser({ ...user, apellidos: value }) }}
                                    />

                                    <TextField
                                        margin="normal"
                                        size='small'
                                        required
                                        fullWidth
                                        id="email"
                                        label="Email"
                                        name="email"
                                        autoComplete="email"
                                        value={user.email}
                                        onChange={({ target: { value } }) => { setUser({ ...user, email: value }) }}
                                    />
                                    <TextField
                                        margin="normal"
                                        size='small'
                                        required
                                        fullWidth
                                        id="username"
                                        label="Username"
                                        name="username"
                                        value={user.username}
                                        onChange={({ target: { value } }) => { setUser({ ...user, username: value }) }}
                                    />
                                </Box>
                                <LoadingButton loading={isUpdating} text="Guardar" onClick={saveChanges} startIcon={<SaveIcon />} />
                            </Box>
                        </Container>
                    </FormCard>
            }

        </MainCard>
    );
}

export default EditUser;