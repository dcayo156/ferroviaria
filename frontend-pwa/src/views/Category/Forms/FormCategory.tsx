import * as React from 'react';
import FormCard from '../../../components/cards/FormCard';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { Box, Button, Checkbox, Container, CssBaseline, FormControl, FormControlLabel, FormGroup, FormLabel, Grid,  InputLabel,  MenuItem,  OutlinedInput,  Select,  SelectChangeEvent,  TextField } from '@mui/material';
import { ICategory } from '../../../store/types/Category';
import { useGetListCategoryQuery } from '../../../store/services/Category';
interface FormCategoryProps {
    category: ICategory
    setCategory: (value: React.SetStateAction<ICategory>) => void
    isCreate:boolean
}
const FormCategory: React.FunctionComponent<FormCategoryProps> = ({ category, setCategory, isCreate }) => {
    const { data: categoryData, error, isLoading } = useGetListCategoryQuery();
    const [enableSelect,setEnabledSelect]=React.useState<boolean>(false)
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
    const handleChangeSelect= (event: SelectChangeEvent)=>{
        setCategory({ ...category, "parentCategoryId": event.target.value })
    }
    React.useEffect(()=>{
        if(!isCreate){
            setEnabledSelect(category.parentCategoryId!=undefined)
        }
    },[category])
    
  return  <FormCard
    icon={<AccountCircleIcon sx={{ fontSize: 50 }} />}
    style={undefined}
    title='Registrar Categoria'
    key='category'
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
                    value={category!.name}
                    onChange={({ target: { value } }) => setCategory({ ...category, "name": value })}
                />  
                    <FormControlLabel onChange={()=>setEnabledSelect(!enableSelect)} control={<Checkbox checked={enableSelect} />} label="es subcategoria?" />
                    <FormControl sx={{ width: "100%" }}>
                    <InputLabel id="select-categoryl">Categoria Padre</InputLabel>
                    <Select
                    disabled={!enableSelect}
                    id="select-category"
                    value={category.parentCategoryId || '' }
                    onChange={handleChangeSelect}
                    input={<OutlinedInput label="Categoria Padre" />}
                    MenuProps={MenuProps}
                    name={"parentCategoryId"}
                    >
                        <MenuItem
                        key="undefiend"
                        value={undefined}
                        >
                        Seleccione una opcion
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
                
                              
            </Box>
        </Box>          
    </Container>
</FormCard>
}
export default FormCategory;