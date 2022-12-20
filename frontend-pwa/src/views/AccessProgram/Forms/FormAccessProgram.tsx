import * as React from 'react';
import FormCard from '../../../components/cards/FormCard';
import { IUserRequest } from '../../../store/types/Auth';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { Box, Button, Container, CssBaseline, Grid,  TextField } from '@mui/material';
import { IAccessProgramRequest } from '../../../store/types/AccessProgram';
interface FormAccessProgramProps {
    accessprogram: IAccessProgramRequest
    setAccessProgram: (value: React.SetStateAction<IAccessProgramRequest>) => void
    isCreate:boolean
}
const FormAccessProgram: React.FunctionComponent<FormAccessProgramProps> = ({ accessprogram, setAccessProgram, isCreate }) => {
    const [errorPassword,setErrorPassword] = React.useState<boolean>(false);
    const [errorConfirmPassword,setErrorConfirmPassword] = React.useState<boolean>(false);
    
    
   

  return  <FormCard
    icon={<AccountCircleIcon sx={{ fontSize: 50 }} />}
    style={undefined}
    title='Registrar Nuevo Acceso a Programa'
    key='access-program'
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
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    id="name"
                    label="Name"
                    name="name"
                    value={accessprogram!.name}
                    onChange={({ target: { value } }) => setAccessProgram({ ...accessprogram, "name": value })}
                />  
                <Grid container>
                    <Grid item xs >
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            id="icon"
                            label="Icon"
                            name="icon"
                            autoFocus
                            value={accessprogram!.iconName}
                            onChange={({ target: { value } }) => setAccessProgram({ ...accessprogram, "iconName": value })}
                        />
                    </Grid>
                    <Grid item >
                        <input
                        accept="image/*"
                        style={{ display: 'none' }}
                        id="raised-button-file"
                        multiple
                        type="file"
                        />
                        <br />
                        <label htmlFor="raised-button-file">
                        <Button variant="outlined" component="span">
                            Upload
                        </Button>
                        </label> 
                    </Grid>
                </Grid>

                <TextField
                    margin="normal"
                    required
                    fullWidth
                    id="url"
                    label="URL"
                    name="url"
                    value={accessprogram!.url}
                    onChange={({ target: { value } }) => setAccessProgram({ ...accessprogram, "url": value })}
                />                
                
                              
            </Box>
        </Box>          
    </Container>
</FormCard>
}
export default FormAccessProgram;