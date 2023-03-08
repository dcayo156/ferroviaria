import * as React from 'react';
import FormCard from '../../../components/cards/FormCard';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { Box, Button, Container, CssBaseline, FormControl, FormLabel, Grid,  InputLabel,  Select,  TextField, MenuItem, OutlinedInput, SelectChangeEvent } from '@mui/material';
import { IDocument,IDocumentImage,IDocumentImageResponse } from '../../../store/types/Document';
import defaultIcon from '../../../../src/assets/img/default-icon.png'
import { URL_API_V1 } from "../../../store/services";
import { useGetListCategoryQuery } from '../../../store/services/Category';
import { useCreateFileDocumentMutation } from '../../../store/services/Document';
import FileLoadingButton from '../../../components/Buttons/FileLoadingButton';
import { toast } from 'react-toastify';
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';
import { Console } from 'console';
interface FormDocumentProps {
    document: IDocument
    setDocument: (value: React.SetStateAction<IDocument>) => void
    isCreate:boolean
}
function usePrevious(value:any) {
    const ref = React.useRef();
    React.useEffect(() => {
      ref.current = value;
    });
    return ref.current;
  }
const FormDocument: React.FunctionComponent<FormDocumentProps> = ({ document, setDocument, isCreate }) => {
    const { data: categoryData, error, isLoading } = useGetListCategoryQuery();
    const [createFile]=useCreateFileDocumentMutation();
    const [createImage]=useCreateFileDocumentMutation();
    const [lastFileName,setlastFileName]=React.useState("");
    const [loadFile,setLoadFile]=React.useState(false);
    const [fileSave,setFileSave]=React.useState<IDocumentImage>({
        file:"",
        fileName:"",
        filePath:"",
        id:"",
        isFile:true
    });
    const [lastImageName,setlastImageName]=React.useState("");
    const [loadImage,setLoadImage]=React.useState(false);
    const [imageSave,setImageSave]=React.useState<IDocumentImage>({
        file:"",
        fileName:"",
        filePath:"",
        id:"",
        isFile:false
    }); 
    
   const [image,setImage]=React.useState< string | undefined >("");
    const ITEM_HEIGHT = 48;
    const ITEM_PADDING_TOP = 8;
    const MenuProps = {
        PaperProps: {
            style: {
            maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
            width: 250,
            },
        },
    };
    React.useEffect(()=>{
        if(fileSave.fileName!=lastFileName){
            setlastFileName(fileSave.fileName);
            createFile(fileSave).then((response: { data: IDocumentImageResponse } | { error: FetchBaseQueryError | SerializedError; })=>{
                if("data" in response){
                    toast.success("archivo creado correctamente correcto");
                    setDocument({...document,filePath:response.data.filePath,fileName:response.data.fileName})
                }
                if("error" in response){
                    if("message" in response.error)
                        toast.error(response.error.message);
                    if("error" in response.error)
                        toast.error(response.error.error);
                }
                setLoadFile(false);
            }); 
        }
    },[fileSave])
    const onFileChange= (e:any) => {
        
        var file =  e.target.files[0];
        if(file){
            var reader = new FileReader();
            reader.onload = (e) => {
                setLoadFile(true);
                setFileSave({ ...fileSave,filePath:document.subCategoryId, fileName: file.name,file:(reader.result?reader.result as string:"") });
            }
            reader.readAsDataURL(file)
        }
    }
    React.useEffect(()=>{
        if(imageSave.fileName!=lastImageName){
            setlastImageName(imageSave.fileName);
            createImage(imageSave).then((response: { data: IDocumentImageResponse } | { error: FetchBaseQueryError | SerializedError; })=>{
                if("data" in response){
                    toast.success("archivo creado correctamente correcto");
                    setDocument({...document,photoPath:response.data.filePath,photoName:response.data.fileName})
                }
                if("error" in response){
                    if("message" in response.error)
                        toast.error(response.error.message);
                    if("error" in response.error)
                        toast.error(response.error.error);
                }
                setLoadImage(false);
            }); 
        }
    },[imageSave])
    const onImageChange= (e:any) => {
        var file =  e.target.files[0];
        if(file){
            var reader = new FileReader();
            reader.onload = (e) => {
                setLoadImage(true);
                setImageSave({ ...fileSave, fileName: file.name,file:(reader.result?reader.result as string:"") });
            }
            reader.readAsDataURL(file)
        }
    }
    const handleChangeChildrenSelect = (event: SelectChangeEvent) => {
        setDocument({ ...document, subCategoryId: event.target.value })
    }
    const handleChangeParentSelect= (event: SelectChangeEvent)=>{
        setDocument({ ...document, categoryId: event.target.value,subCategoryId:"" })
    }

  return  <FormCard
    icon={<AccountCircleIcon sx={{ fontSize: 50 }} />}
    style={undefined}
    title='Registrar Nuevo Documentos'
    key='documnent'
    action={null}>
    <Container sx={{pb:3}}
        component="main" maxWidth="xs" >
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
                noValidate
                sx={{ mt: 1 }}
            >
                <FormControl sx={{ width: "100%",pb:2 }}>
                    <InputLabel id="select-category-label-1">Categoria Padre</InputLabel>
                        <Select
                        id="select-category-1"
                        value={document.categoryId || '' }
                        onChange={handleChangeParentSelect}
                        input={<OutlinedInput label="Categoría Padre" />}
                        MenuProps={MenuProps}
                        name={"parentCategoryId"}
                        >
                            <MenuItem
                            key="undefiend"
                            value={undefined}
                            >
                            Seleccione una Categoría
                            </MenuItem>
                            {
                                categoryData?.filter(cat=>cat.parentCategoryId==undefined).map(cat=>{
                                    return <MenuItem
                                    key={cat.id}
                                    value={cat.id}
                                    >
                                    {cat.name}
                                    </MenuItem>
                                })
                            }
                        </Select>
                </FormControl>
                <FormControl sx={{ width: "100%",pb:2 }}>
                    <InputLabel id="select-category-label-2">Categoría hijo</InputLabel>
                        <Select
                        id="select-category-2"
                        value={document.subCategoryId || '' }
                        onChange={handleChangeChildrenSelect}
                        input={<OutlinedInput label="Categoría hijo" />}
                        MenuProps={MenuProps}
                        name={"subCategoryId"}
                        >
                            <MenuItem
                            key="undefiend"
                            value={undefined}
                            >
                            Seleccione una Subcategoría
                            </MenuItem>
                            {
                                categoryData?.filter(cat=>cat.parentCategoryId==document.categoryId).map(cat=>{
                                    return <MenuItem
                                    key={cat.id}
                                    value={cat.id}
                                    >
                                    {cat.name}
                                    </MenuItem>
                                })
                            }
                        </Select>
                </FormControl>
                <Grid container direction="column" justifyContent="center" alignItems="center">
                    <Grid item >
                        <Grid container>
                            <Grid item>
                                <input
                                    accept="application/*"
                                    style={{ display: 'none' }}
                                    id="raised-button-file"
                                    multiple
                                    type="file"
                                    onChange={(e)=> onFileChange(e) }
                                    />
                                    <label htmlFor="raised-button-file">
                                    <FileLoadingButton
                                        loading={loadFile}
                                        text="Subir"/>
                                    </label> 
                            </Grid>
                            <Grid item style={{marginLeft:3, textOverflow:"ellipsis",overflow:"hidden",width:"200px",whiteSpace:"nowrap"}}>
                                <label >
                                    {
                                    document.fileName == "" ?
                                    "Seleccionar un archivo":
                                    document.fileName
                                    }
                                    
                                </label>
                            </Grid>
                        </Grid>
                        
                    </Grid>
                </Grid>
                <Grid container direction="column" justifyContent="center" alignItems="center">
                    <Grid item >
                        <Grid container>
                            <Grid item>
                                <input
                                    accept="image/*"
                                    style={{ display: 'none' }}
                                    id="raised-button-image"
                                    multiple
                                    type="file"
                                    onChange={(e)=> onImageChange(e) }
                                    />
                                    <label htmlFor="raised-button-image">
                                    <FileLoadingButton
                                        loading={loadImage}
                                        text="Subir"/>
                                    </label> 
                            </Grid>
                            <Grid item style={{marginLeft:3, textOverflow:"ellipsis",overflow:"hidden",width:"200px",whiteSpace:"nowrap"}}>
                                <label >
                                    {
                                    document.photoName == "" ?
                                    "Seleccionar una imagen":
                                    document.photoName
                                    }
                                    
                                </label>
                            </Grid>
                        </Grid>
                        
                    </Grid>
                </Grid>
            </Box>
        </Box>          
    </Container>
</FormCard>
}
export default FormDocument;