import { Grid } from '@mui/material';
import * as React from 'react';
import CardButton from '../../components/cards/CardButton';
import MainCard from '../../components/cards/MainCard';
import SaveIcon from '@mui/icons-material/Save'
import { useGetUserByIDQuery, useUpdateRegisterMutation} from '../../store/services/Auth'
import LoadingButton from '../../components/Buttons/LoadingButton';
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';
import { toast } from 'react-toastify';
import { hasError } from '../../components/Security/ErrorManager';
import { IRegisterResponse, IUserRequest } from '../../store/types/Auth'
import { useNavigate, useParams } from 'react-router-dom';
import UserData from './Forms/UserData';

const EditUser: React.FunctionComponent= () => {
    const { id }: any = useParams();
    const [isLoading,setIsLoading]=React.useState(false);
    const navigate = useNavigate();
    const [userEdit, setUserEdit] = React.useState<IUserRequest| Partial<IUserRequest>>({
        id:id,
        nombre: '',
        apellidos: '',
        email: '',
        username:'',
        password: '',
        confirmpassword: ''
    });
    const {data} = useGetUserByIDQuery(id as string,{ skip: id==null});

    React.useEffect(()=>{
        data && setUserEdit(data)
    },[data])

    const [updateRegister] = useUpdateRegisterMutation();   
    const saveChanges = () => {
        const userToEdit: IUserRequest = {
            id:userEdit.id!,
            nombre: userEdit.nombre!,
            apellidos: userEdit.apellidos!,
            email: userEdit.email!,
            username:userEdit.username!,
            password: "",
            confirmpassword: ""
          };
        setIsLoading(true)
        updateRegister(userToEdit).then((response: { data: IRegisterResponse } | { error: FetchBaseQueryError | SerializedError; }) => {
            if (hasError(response, "Error al momento de crear el Usuario")) {
                return;
            }
            if ("data" in response) {
                toast.success("Registro exitoso");
                navigate("/user");
            }
            setIsLoading(false)
        });
    }
    return (
    <MainCard 
        title="Editar usuario" 
        secondary={
            <CardButton type="back" title="Lista de Usuarios" link="/user" />}
    >{
        <>
        <UserData user={userEdit} setUser={setUserEdit} isCreate={false}/>
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

export default EditUser;