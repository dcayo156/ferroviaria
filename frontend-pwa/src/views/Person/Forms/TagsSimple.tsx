import React, { Component, useEffect } from 'react';
import Grid from '@mui/material/Grid'
import { useGetTagCategoryQuery } from '../../../store/services/Tag'
import TagIcon from '@mui/icons-material/LocalOffer';
import FormCard from '../../../components/cards/FormCard'
import { useTheme } from '@mui/material/styles';
import { selectValuesProps } from '../../../store/types/delete-Tag';
import TextField from "@mui/material/TextField";
import Autocomplete from "@mui/material/Autocomplete";

interface TagsProps {
    valueCategories: selectValuesProps[] 
    selectValue: selectValuesProps[]    
    setSelectValue: (valueTags: React.SetStateAction<selectValuesProps[] >) => void
}

const Tags: React.FunctionComponent<TagsProps> = ({valueCategories, selectValue , setSelectValue}) => {
    const parentTheme = useTheme(); 
    return (
        <FormCard
            style={{ minHeight: "100%",overflow:"inherit" }}
            icon={<TagIcon />}
            title='Seleccionar Tag de Personas'
            key='selectTagPeople'
            action={null}>
            <Grid container spacing={2} >
                <Grid item xs={12}>                    
                    <Autocomplete
                        multiple
                        disabled={valueCategories.length === 0 ? true:false}  
                        onChange={(event, value) => setSelectValue(value)}
                        value={selectValue}                                             
                        options={valueCategories}
                        groupBy={(option) => option.labelCategory}                
                        getOptionLabel={(option) => option.label}   
                        isOptionEqualToValue={(option, value) => option.label === value.label}                    
                        sx={{  spacing: {
                            controlHeight: 48
                            },
                            colors: {
                                primary: parentTheme.palette.primary.main,
                            } }}  
                        renderInput={(params) => (
                            <TextField {...params} label="Seleccionar..." />
                          )}            
                    />
                </Grid>                
            </Grid>            
        </FormCard>
    );
}   
export default Tags;
