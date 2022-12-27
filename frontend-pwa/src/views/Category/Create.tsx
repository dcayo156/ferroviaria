import * as React from "react";
import { useState } from 'react';
import Grid from "@mui/material/Grid";
import { useNavigate} from 'react-router-dom'
import { useCreateCategoryMutation} from '../../store/services/Category'
import { toast } from 'react-toastify';
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query";
import { SerializedError } from "@reduxjs/toolkit";
import MainCard from '../../components/cards/MainCard';
import CardButton from '../../components/cards/CardButton';
import FormCategory from "./Forms/FormCategory";
import SaveIcon from '@mui/icons-material/Save';
import { ICategory } from '../../store/types/Category';
import LoadingButton from "../../components/Buttons/LoadingButton";
export default function Register() {
    const navigate =  useNavigate();
    const [category, setCategory] = useState<ICategory>({
        id:'',
        name:"",
        parentCategoryId:undefined
    });
    const [registerCategory] = useCreateCategoryMutation();
    const [isLoading,setIsLoading]=React.useState(false);
    const isURL=(str:string)=> {
        var pattern = new RegExp('^(https?:\\/\\/)?'+ // protocol
        '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.?)+[a-z]{2,}|'+ // domain name
        '((\\d{1,3}\\.){3}\\d{1,3}))'+ // OR ip (v4) address
        '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*'+ // port and path
        '(\\?[;&a-z\\d%_.~+=-]*)?'+ // query string
        '(\\#[-a-z\\d_]*)?$','i'); // fragment locator
        return pattern.test(str);
      }
    const saveChanges = () => { 
        setIsLoading(true)
          registerCategory(category).then((response: { data: string } | { error: FetchBaseQueryError | SerializedError; })=>{
            if("data" in response){
                toast.success("Registro exitoso");
                navigate("/category");
            }
            if("error" in response){
                if("message" in response.error)
                    toast.error(response.error.message);
                if("error" in response.error)
                    toast.error(response.error.error);
            }
            setIsLoading(false)
        });
    }
    return (
    <MainCard 
        title="Registrar Categoria" 
        secondary={
            <CardButton type="back" title="Lista de Categorias" link="/category" />}
    >{
        <>
        <FormCategory category={category} setCategory={setCategory} isCreate= {true}/>
        <Grid item xs={12} md={12} display="flex" justifyContent="center">
            <LoadingButton
                loading={isLoading}
                text="Guardar"
                onClick={saveChanges}
                startIcon={<SaveIcon />}/>
        </Grid>                    
    </>   
    }
    </MainCard>);
}