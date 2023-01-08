import * as React from "react";
import { useState } from 'react';
import Grid from "@mui/material/Grid";
import { useNavigate, useParams} from 'react-router-dom'
import { useCreateProgramMutation, useGetFindProgramByIdQuery,useUpdateProgramMutation } from '../../store/services/AccessProgram'
import { toast } from 'react-toastify';
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query";
import { SerializedError } from "@reduxjs/toolkit";
import MainCard from '../../components/cards/MainCard';
import CardButton from '../../components/cards/CardButton';
import FormAccessProgram from "./Forms/FormAccessProgram";
import SaveIcon from '@mui/icons-material/Save';
import { IAccessProgram } from '../../store/types/AccessProgram';
import LoadingButton from "../../components/Buttons/LoadingButton";
export default function AccessProgramEdit() {
    const { id }: any = useParams();
    const { data, error, isLoading:load } = useGetFindProgramByIdQuery(id);
    const navigate =  useNavigate();
    const [accessProgram, setAccessProgram] = useState<IAccessProgram>({
        id:'',
        file:"",
        iconName:"",
        name:"",
        url:""
    });
    React.useEffect(()=>{
        if(data!=undefined){
            setAccessProgram({
                id:data.id,
                file:data.file,
                iconName:data.iconName,
                name:data.name,
                url:data.url
            })
        }
    },[data])
    const [registerAccessProgram] = useCreateProgramMutation();
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
      const [updateProgram] = useUpdateProgramMutation();
    const saveChanges = () => { 
        setIsLoading(true)
        const useToCreate: IAccessProgram = {
            id:accessProgram.id,
            iconName:accessProgram.iconName,
            file:accessProgram.file,
            name:accessProgram.name,
            url:accessProgram.url
          };
          updateProgram(useToCreate).then((response: { data: IAccessProgram } | { error: FetchBaseQueryError | SerializedError; })=>{
            if("data" in response){
                toast.success("Registro exitoso");
                navigate("/access-program");
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
        title="Registrar Acceso de Programa" 
        secondary={
            <CardButton type="back" title="Lista de Acceso a Programas" link="/access-program" />}
    >{
        <>
        <FormAccessProgram accessprogram={accessProgram} setAccessProgram={setAccessProgram} isCreate= {false}/>
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