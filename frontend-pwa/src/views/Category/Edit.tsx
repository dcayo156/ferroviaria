import * as React from "react";
import { useState } from 'react';
import Grid from "@mui/material/Grid";
import { useNavigate, useParams} from 'react-router-dom'
import { useCreateCategoryMutation, useGetFindCategoryByIdQuery, useUpdateCategoryMutation} from '../../store/services/Category'
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
    const { id }: any = useParams();
    const { data, error, isLoading:load } = useGetFindCategoryByIdQuery(id);
    const [category, setCategory] = useState<ICategory>({
        id:'',
        name:"",
        parentCategoryId:undefined
    });
    React.useEffect(()=>{
        if(data!=undefined){
            setCategory({
                id:data.id,
                name:data.name,
                parentCategoryId:data.parentCategoryId
            })
        }
    },[data])
    const [updateCategory] = useUpdateCategoryMutation();
    const [isLoading,setIsLoading]=React.useState(false);
    
    const saveChanges = () => { 
        setIsLoading(true)
          updateCategory(category as ICategory).then((response: { data: ICategory } | { error: FetchBaseQueryError | SerializedError; })=>{
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
        title="Editar Categoria" 
        secondary={
            <CardButton type="back" title="Lista de Categorias" link="/category" />}
    >{
        <>
        <FormCategory category={category} setCategory={setCategory} isCreate={false}/>
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