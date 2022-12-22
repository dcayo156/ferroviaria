import React, { Component, useEffect } from 'react';
import Grid from '@mui/material/Grid'
import CreatableSelect from 'react-select/creatable';
import { useCreateTagMutation,useGetTagsQuery,useGetTagCategoryQuery } from '../../../store/services/Tag'
import TagIcon from '@mui/icons-material/LocalOffer';
import FormCard from '../../../components/cards/FormCard'
import { useTheme } from '@mui/material/styles';
import { toast } from 'react-toastify';
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';
import { ITag } from '../../../store/types/delete-Tag';
import DialogCreateTag from '../../Tag/Forms/DialogCreate'
interface TagsProps {
    tags: ITag[] 
    setTags: (value: React.SetStateAction<ITag[] >) => void
}
interface selectValuesProps {
    value: string
    label: string
    __isNew__: boolean
}
const Tags: React.FunctionComponent<TagsProps> = ({tags,setTags}) => {
    const { data: categories, error:categorie_error, isLoading:categorie_esLoading } = useGetTagCategoryQuery();
    const [categoryId,setCategoryId] = React.useState<string>("");
    const [tagName,setTagName]=React.useState<string>("");
    const [openDialog,setOpenDialog]=React.useState<boolean>(false);
    const [selectValues, setSetlectValues] = React.useState<selectValuesProps[]>([]);
    const [value, setValue] = React.useState<selectValuesProps[]>([]);
    const { data, error, isLoading } = useGetTagsQuery();
    const [createTag, { isLoading: isUpdating }] = useCreateTagMutation();
    const parentTheme = useTheme();
    const [createTagHere,setCreateTagHere]=React.useState(false);
    const [inAwait,setInAwait]=React.useState<selectValuesProps|null>(null);
    React.useEffect(() => {
        if (!data) { return }
        setSetlectValues(data.map((tag) => {
            const select: selectValuesProps = { value: tag.id, label: tag.name, __isNew__: false }
            return select
        }));
    }, [data])
    React.useEffect(()=>{
        if(categories && categories?.length > 0){
            setCategoryId(categories.at(0)?.id as string)
        }
    },[categories])
    useEffect(()=>{
        setTags(value.map(v=>({id:v.value,
            name:v.label,
            description:"",
            tagCategoryId:categoryId})))
    },[value])
    const handleChange = (newValue: any, actionMeta: any) => {
        switch (actionMeta.action) {
            case "select-option":
                setValue(newValue)
                break;
            case "remove-value":
                setValue(newValue)
                break;
            case "create-option":
                const find: selectValuesProps = newValue.find((n: selectValuesProps) => n.__isNew__)
                const newtag: ITag = {
                    description: "",
                    id: "",
                    name: find.label,
                    tagCategoryId: categoryId
                }
                setInAwait(find);
                if(createTagHere){
                    createTag(newtag).then((response: { data: string } | { error: FetchBaseQueryError | SerializedError; }) => {
                        if ("data" in response) {
                            find!.value=response.data;
                            find!.__isNew__=false;
                            setValue([...value,find])
                            toast.success(`Tag creado exitosamente`)
                        }
                        if ("error" in response) 
                        {
                            console.log(response)
                            toast.error("Error al momento de crear el Tag");
                        }
                        setInAwait(null);
                    })
                }else{
                    setTagName(find.label);
                    setOpenDialog(true);
                }
                
                break;
        }

        
    }
    const reciveResponse=(res:boolean,response:any)=>{
        if(res && inAwait!=null){
            inAwait!.value=response.data;
            inAwait!.__isNew__=false;
            setValue([...value,inAwait])
            setInAwait(null)
        }
    }
    const onChangeOption=()=>{
        setCreateTagHere(!createTagHere);
    }
    return (
        <FormCard
            style={{ minHeight: "100%",overflow:"inherit" }}
            icon={<TagIcon />}
            title='Registrar Tag de Personas'
            key='registerTagPeople'
            action={null}>
            <Grid container spacing={2} >
                <Grid item xs={12}>
                    <div style={{fontSize:".8em",cursor:"pointer",textDecoration:"underline"}} 
                        onClick={onChangeOption}>
                            {
                                createTagHere?`Crear tag en una categoria por defecto`:`Seleccionar la categoria antes de crear`
                            }
                            </div>
                    <CreatableSelect
                        isMulti
                        isDisabled={categoryId===""?true:false}
                        value={value}
                        onChange={handleChange}
                        options={selectValues}
                        theme={(theme) => ({
                            ...theme,
                            spacing: {
                                ...theme.spacing,
                                controlHeight: 48
                            },
                            colors: {
                                ...theme.colors,
                                primary: parentTheme.palette.primary.main,
                            },
                        }
                        )}
                    />
                </Grid>
                {
                    openDialog && <DialogCreateTag name={tagName} categoryId={categoryId} openDialog={openDialog} setOpenDialog={setOpenDialog} sendResponse={reciveResponse} key="createTag"/>
                }
            </Grid>
            
        </FormCard>
    );
}

export default Tags;
