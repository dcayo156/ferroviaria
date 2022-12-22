import * as React from 'react';
import FormCard from '../../../components/cards/FormCard';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { Box, Button, Container, CssBaseline, FormLabel, Grid,  TextField } from '@mui/material';
import { IAccessProgram } from '../../../store/types/AccessProgram';
import defaultIcon from '../../../../src/assets/img/default-icon.png'
interface FormAccessProgramProps {
    accessprogram: IAccessProgram
    setAccessProgram: (value: React.SetStateAction<IAccessProgram>) => void
    isCreate:boolean
}
const FormAccessProgram: React.FunctionComponent<FormAccessProgramProps> = ({ accessprogram, setAccessProgram, isCreate }) => {
    const [errorPassword,setErrorPassword] = React.useState<boolean>(false);
    const [errorConfirmPassword,setErrorConfirmPassword] = React.useState<boolean>(false);
    
    
   const [image,setImage]=React.useState< string | undefined >("");
   const onFileChange= (e:any) => {
        var file =  e.target.files[0];
        if(file){
            const pattern = /imagen-*/;
            var reader = new FileReader();
            
            reader.onload = (e) => {
                setImage(reader.result?reader.result as string:"")
                setAccessProgram({ ...accessprogram, "iconName": file.name,file:(reader.result?reader.result as string:"") }) 
            }
            //setAccessProgram({ ...accessprogram, "iconName": file.name,file:reader.result as string }) 
            reader.readAsDataURL(file)
        }
   }
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
                <Grid container direction="column" justifyContent="center" alignItems="center">
                    
                    <Grid item>
                        <label title="Cambiar Foto" style={{width:"250px",height:"250px"}} htmlFor="raised-button-file">
                            <img className="" src={image!="" ?image:defaultIcon} alt="" style={{width:"250px",height:"250px"}} />
                        </label>
                    
                    </Grid>
                    <Grid item >
                        <Grid container>
                            <Grid item>
                                <input
                                    accept="image/*"
                                    style={{ display: 'none' }}
                                    id="raised-button-file"
                                    multiple
                                    type="file"
                                    onChange={(e)=> onFileChange(e) }
                                    />
                                    <label htmlFor="raised-button-file">
                                    <Button variant="outlined" component="span"  >
                                        Subir
                                    </Button>
                                    </label> 
                            </Grid>
                            <Grid item style={{marginLeft:3, textOverflow:"ellipsis",overflow:"hidden",width:"200px",whiteSpace:"nowrap"}}>
                                <label >
                                    {
                                    accessprogram.iconName == "" ?
                                    "Seleccionar una imagen":
                                    accessprogram.iconName
                                    }
                                    
                                </label>
                            </Grid>
                        </Grid>
                        
                    </Grid>
                </Grid>
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