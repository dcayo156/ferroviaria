import * as React from "react";
import { useState } from 'react';
import Grid from "@mui/material/Grid";
import { useNavigate, useParams} from 'react-router-dom'
import { useGetFindDocumentByIdQuery,useUpdateDocumentMutation } from '../../store/services/Document'
import { toast } from 'react-toastify';
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query";
import { SerializedError } from "@reduxjs/toolkit";
import MainCard from '../../components/cards/MainCard';
import CardButton from '../../components/cards/CardButton';
import FormDocument from "./Forms/FormDocument";
import SaveIcon from '@mui/icons-material/Save';
import { IDocument } from '../../store/types/Document';
import LoadingButton from "../../components/Buttons/LoadingButton";
export default function DocumentEdit() {
    const { id }: any = useParams();
    const { data, error, isLoading:load } = useGetFindDocumentByIdQuery(id);
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
    React.useEffect(()=>{
        console.log("data en document");
        console.log(data);
        if(data!=undefined){
            setDocument({
                id:data.id,
                fileName: data.fileName,
                filePath: data.filePath,
                photoName: data.photoName,
                photoPath: data.photoPath,
                categoryId: data.categoryId,
                subCategoryId: data.subCategoryId
            })
        }
    },[data]);
    const [isLoading,setIsLoading]=React.useState(false);
    const [updateDocument] = useUpdateDocumentMutation();
    const saveChanges = () => { 
        setIsLoading(true)
        const useToCreate: IDocument = {
            id:document.id,
            fileName: document.fileName,
            filePath: document.filePath,
            photoName: document.photoName,
            photoPath: document.photoPath,
            categoryId: document.categoryId,
            subCategoryId: document.subCategoryId
          };
          updateDocument(useToCreate).then((response: { data: IDocument } | { error: FetchBaseQueryError | SerializedError; })=>{
            if("data" in response){
                toast.success("Edicion exitosa");
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
        title="Editar registro de documento" 
        secondary={
            <CardButton type="back" title="Lista de Documentos" link="/document" />}
    >{
        <>
        <FormDocument document={document} setDocument={setDocument} isCreate= {false}/>
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