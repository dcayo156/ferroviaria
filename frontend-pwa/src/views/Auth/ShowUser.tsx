import { Box, Grid, Button, Typography, Container, styled, LinearProgress } from '@mui/material';
import * as React from 'react';
import FormCard from '../../components/cards/FormCard';
import MainCard from '../../components/cards/MainCard';
import LockIcon from '@mui/icons-material/Lock'
import { useNavigate } from 'react-router-dom';
import { useGetUserByIDQuery } from '../../store/services/Auth';
import { getCredentials } from '../../store/slices/Auth/localStorage';
interface ShowUserProps {

}
const BoxShadow = styled(Box)(({ theme }) => ({
    display: "flex",
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    padding: theme.spacing(0.5),
    boxShadow: `1px 0px 3px 0px ${theme.palette.secondary.main}`
}));
const BoxTitle = styled(Box)(({ theme }) => ({
    fontSize: "1.2em", fontWeight: "bold", paddingRight: "1em", flex: 3
}))
const BoxDetails = styled(Box)(({ theme }) => ({
    fontSize: "1.2em", fontStyle: "italic", paddingTop: ".3em", flex: 7
}))
const ShowUser: React.FunctionComponent<ShowUserProps> = () => {
    const {id}= getCredentials();
    const { data:currentUser, error, isLoading } = useGetUserByIDQuery(id as string,{
        skip: id==null,  
    });
    const navigate = useNavigate();
    const goTo = (url: string) => {
        navigate(url);
    }
    return (
        <MainCard title="Informacion de Usuario" secondary={null} >
            {
                isLoading ?
                    <LinearProgress color="secondary" />
                    :
                    <FormCard
                        icon={<LockIcon />}
                        style={undefined}
                        title='Mis Datos'
                        key='my information'
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
                                <Typography component="h1" variant="h4" textAlign={"center"}>
                                    Mis Datos de Usuario
                                </Typography>
                                <Box
                                    component="form"
                                    onSubmit={() => { }}
                                    noValidate
                                    sx={{ mt: 1 }}
                                >
                                    <Grid container rowGap={2}>
                                        <Grid item xs={12} >
                                            <BoxShadow >
                                                <BoxTitle>Nombre:</BoxTitle>
                                                <BoxDetails>{currentUser!.nombre}</BoxDetails>
                                            </BoxShadow>
                                        </Grid>
                                        <Grid item xs={12}>
                                            <BoxShadow >
                                                <BoxTitle>Apellidos:</BoxTitle>
                                                <BoxDetails>{currentUser!.apellidos}</BoxDetails>
                                            </BoxShadow>
                                        </Grid>
                                        <Grid item xs={12}>
                                            <BoxShadow >
                                                <BoxTitle>Email:</BoxTitle>
                                                <BoxDetails>{currentUser!.email}</BoxDetails>
                                            </BoxShadow>
                                        </Grid>
                                        <Grid item xs={12}>
                                            <BoxShadow >
                                                <BoxTitle>User Name:</BoxTitle>
                                                <BoxDetails>{currentUser!.username}</BoxDetails>
                                            </BoxShadow>
                                        </Grid>
                                        <Grid item xs={12} columnGap={1}>
                                            <Box display="flex" justifyContent={"center"} gap={1}>
                                                <Button variant="contained" onClick={() => goTo(`/auth/edit/${currentUser!.id}`)}>
                                                    Editar mis datos
                                                </Button>
                                                <Button variant="contained" onClick={() => goTo(`/auth/change-password/${currentUser!.id}`)} >
                                                    Cambiar Contraseña
                                                </Button>
                                            </Box>
                                        </Grid>
                                    </Grid>
                                </Box>
                            </Box>
                        </Container>
                    </FormCard>
            }
        </MainCard>);
}

export default ShowUser;