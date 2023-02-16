import * as React from 'react';
import FormCard from '../../../components/cards/FormCard';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { Box, Button, Container, CssBaseline, FormControl, FormLabel, Grid,  InputLabel,  Select,  TextField, MenuItem, OutlinedInput, SelectChangeEvent } from '@mui/material';
import { IInspectionTrainCreate,IInspectionTrainImage,IInspectionTrainImageResponse } from '../../../store/types/InspectionTrain';
import defaultIcon from '../../../../src/assets/img/default-icon.png'
import { URL_API_V1 } from "../../../store/services";
import { useGetListCategoryQuery } from '../../../store/services/Category';
import { useCreateFileDocumentMutation } from '../../../store/services/Document';
import FileLoadingButton from '../../../components/Buttons/FileLoadingButton';
import { toast } from 'react-toastify';
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';
import { Console } from 'console';
interface FormInspectionTrainProps {
    inspectionTrain: IInspectionTrainCreate
    setInspectionTrain: (value: React.SetStateAction<IInspectionTrainCreate>) => void
    isCreate:boolean
}
function usePrevious(value:any) {
    const ref = React.useRef();
    React.useEffect(() => {
      ref.current = value;
    });
    return ref.current;
  }
const FormInspectionTrain: React.FunctionComponent<FormInspectionTrainProps> = ({ inspectionTrain, setInspectionTrain, isCreate }) => {
    const { data: categoryData, error, isLoading } = useGetListCategoryQuery();
    const [createFile]=useCreateFileDocumentMutation();
    //const [createImage]=useCreateFileDocumentMutation();
    const [lastFileName,setlastFileName]=React.useState("");
    const [loadFile,setLoadFile]=React.useState(false);
    const [fileSave,setFileSave]=React.useState<IInspectionTrainImage>({
        file:"",
        fileName:"",
        filePath:"",
        id:"",
        isFile:true
    });
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
            createFile(fileSave).then((response: { data: IInspectionTrainImageResponse } | { error: FetchBaseQueryError | SerializedError; })=>{
                if("data" in response){
                    toast.success("Archivo creado correctamente correcto");
                    setInspectionTrain({...inspectionTrain,filePath:response.data.filePath,fileName:response.data.fileName})
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
                setFileSave({ ...fileSave, filePath:inspectionTrain.subCategoryId, fileName: file.name,file:(reader.result?reader.result as string:"")});
            }
            reader.readAsDataURL(file)
        }
    }
    // React.useEffect(()=>{
    //     if(imageSave.fileName!=lastImageName){
    //         setlastImageName(imageSave.fileName);
    //         createImage(imageSave).then((response: { data: IInspectionTrainImageResponse } | { error: FetchBaseQueryError | SerializedError; })=>{
    //             if("data" in response){
    //                 toast.success("archivo creado correctamente correcto");
    //                 setInspectionTrain({...document,photoPath:response.data.filePath,photoName:response.data.fileName})
    //             }
    //             if("error" in response){
    //                 if("message" in response.error)
    //                     toast.error(response.error.message);
    //                 if("error" in response.error)
    //                     toast.error(response.error.error);
    //             }
    //             setLoadImage(false);
    //         }); 
    //     }
    // },[imageSave])
    // const onImageChange= (e:any) => {
    //     var file =  e.target.files[0];
    //     if(file){
    //         var reader = new FileReader();
    //         reader.onload = (e) => {
    //             setLoadImage(true);
    //             setImageSave({ ...fileSave, fileName: file.name,file:(reader.result?reader.result as string:"") });
    //         }
    //         reader.readAsDataURL(file)
    //     }
    // }
    const handleChangeChildrenSelect = (event: SelectChangeEvent) => {
        setInspectionTrain({ ...inspectionTrain, subCategoryId: event.target.value })
    }
    const handleChangeParentSelect= (event: SelectChangeEvent)=>{
        setInspectionTrain({ ...inspectionTrain, categoryId: event.target.value,subCategoryId:"" })
    }

  return  <FormCard
    icon={<AccountCircleIcon sx={{ fontSize: 50 }} />}
    style={undefined}
    title='Registrar nueva inspección integral'
    key='inspectionTrain'
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
                        value={inspectionTrain.categoryId || '' }
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
                        value={inspectionTrain.subCategoryId || '' }
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
                                categoryData?.filter(cat=>cat.parentCategoryId==inspectionTrain.categoryId).map(cat=>{
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
                                    inspectionTrain.fileName == "" ?
                                    "Seleccionar un archivo":
                                    inspectionTrain.fileName
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
                    id="codigo"
                    label="Código"
                    name="codigo"
                    value={inspectionTrain!.codigo}
                    onChange={({ target: { value } }) => setInspectionTrain({ ...inspectionTrain, "codigo": value })}
                />  
            </Box>
        </Box>          
    </Container>
</FormCard>
}
export default FormInspectionTrain;