import * as React from "react";
import { useState } from 'react';
import Grid from "@mui/material/Grid";
import { useNavigate} from 'react-router-dom'
import { useCreateDocumentMutation } from '../../store/services/Document'
import { toast } from 'react-toastify';
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query";
import { SerializedError } from "@reduxjs/toolkit";
import MainCard from '../../components/cards/MainCard';
import CardButton from '../../components/cards/CardButton';
import FormDocument from "./Forms/FormDocument";
import SaveIcon from '@mui/icons-material/Save';
import { IDocument } from '../../store/types/Document';
import LoadingButton from "../../components/Buttons/LoadingButton";
export default function Register() {
    const navigate =  useNavigate();
    const [document, setDocument] = useState<IDocument>({
        id:"",
        fileName: "",
        filePath: "",
        photoName: "",
        photoPath: "",
        categoryId: "",
        subCategoryId: ""
    });
    const [registerDocument] = useCreateDocumentMutation();
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
        console.log(document)
        const useToCreate: IDocument = {
            id:"",
            fileName: document.fileName,
            filePath: document.filePath,
            photoName: document.photoName,
            photoPath: document.photoPath,
            categoryId: document.categoryId,
            subCategoryId: document.subCategoryId
          };
          if(useToCreate.categoryId=="" || useToCreate.subCategoryId==""){
            toast.warning("Ingrese todos los datos");
            setIsLoading(false)
            return
          }
            
          registerDocument(useToCreate).then((response: { data: string } | { error: FetchBaseQueryError | SerializedError; })=>{
            if("data" in response){
                toast.success("Registro exitoso");
                navigate("/document");
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
        title="Registrar Documentos" 
        secondary={
            <CardButton type="back" title="Lista de  Documentos" link="/access-program" />}
    >{
        <>
        <FormDocument document={document} setDocument={setDocument} isCreate= {true}/>
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