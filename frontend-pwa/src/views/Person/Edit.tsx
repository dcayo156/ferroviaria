import { Avatar, Box, Button, ButtonGroup, Card, CardActions, CardContent, CardHeader, CardMedia, Collapse, IconButton, LinearProgress, TextField, Typography } from '@mui/material';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import * as React from 'react';
import { red } from '@mui/material/colors';
import SaveIcon from '@mui/icons-material/Save';
import CancelIcon from '@mui/icons-material/Cancel';
import { useGetFindPeopleByIdQuery, useUpdatePeopleMutation } from '../../store/services/Person'
import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom'
import {IPerson, Gender, IMail, IPhone,IPersonWithTag, IPersonSimple,ITagInfo, IAddress, ICommunicationChannels } from '../../store/types/Person'
import MainCard from '../../components/cards/MainCard'
import PersonalData from './Forms/PersonalData'
import CardButton from '../../components/cards/CardButton';
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';
import { toast } from 'react-toastify';
import LoadingButton from '../../components/Buttons/LoadingButton';

import Grid from '@mui/material/Grid';
import PersonalDataSimple from './Forms/PersonalDataSimple'
import Addresses from './Forms/AddressesSingle'
import Tags from './Forms/TagsSimple'
import { ITag, selectValuesProps } from '../../store/types/delete-Tag';
import { cacheNames } from 'workbox-core/_private';
import { stringify } from 'querystring';
import { useGetTagCategoryQuery } from '../../store/services/Tag';
interface EditProps {
}

const Edit = () => {
    const { id }: any = useParams();
    const navigate = useNavigate();
    const [
        updatePerson,
        { isLoading: isUpdating },
    ] = useUpdatePeopleMutation()
    const { data, error, isLoading } = useGetFindPeopleByIdQuery(id);
    const newaddress : IAddress={
        street:"",
        city: 0,
        state: 0,
        country: 0,
        zipCode: "diego",
        latitude: 0,
        longitude: 0,
        description: ""
    }
    const [personToEdit , setPersonToEdit] = React.useState<IPersonSimple | Partial<IPersonSimple>>({
        id: "",
        firstName: "",
        secondName: "",
        lastName: "",
        nationalId: "",
        birthDate: "2022-06-21T20:14:15.005Z",
        gender: Gender.Personalizado,
        pronounPreference: "Femenino",
        addresses: [newaddress],
        email: "",
        phone:  "",
        tags: []
    });    

    const { data: categories, error:categorie_error, isLoading:categorie_esLoading } = useGetTagCategoryQuery();
    const [selectValue,setSelectValue]=React.useState<selectValuesProps[]>([]);
    const [valueCategories,setValueCategories]=React.useState<selectValuesProps[]>([]);
    const [addreses,setAddreses]=React.useState<IAddress>({
            street: "",
            city: 0,
            state: 0,
            country: 0,
            zipCode: "none",
            latitude: 0,
            longitude: 0,
            description: ""
        });
    React.useEffect(() => {
        if (!categories) { return }
        var listCategories : any=[];
        categories.forEach(category => {
            category.tags.forEach(tag =>{
                const select: selectValuesProps = { 
                    value: tag.id, 
                    label: tag.name,
                    labelCategory : category.description,
                    valueCategory:category.id
                }
                listCategories.push(select);
            })   
        });       
        setValueCategories(listCategories);
    }, [categories]) 

    React.useEffect(() => {
        const person : IPersonSimple = {
            id: data?.id,
            firstName: data?.firstName,
            secondName: data?.secondName,
            lastName: data?.lastName,
            nationalId: data?.nationalId,
            birthDate: data?.birthDate,
            gender: data?.gender,
            pronounPreference: data?.pronounPreference,
            addresses: data?.addresses.length! > 0 ? data?.addresses : [newaddress],
            email: filterComunicationChannerls(data?.communicationChannels! ,"Email Personal"),        
            phone: filterComunicationChannerls(data?.communicationChannels! ,"Celular Personal"),
            tags: data?.tags.length! > 0 ? data?.tags : [] as ITagInfo[],            
        }       
        data !== undefined && setPersonToEdit(person);
        const tagsInitial: selectValuesProps[] = valueCategories.filter(function(item) {
            return person?.tags?.find(tag => tag.name.includes(item.label));
        })
        data !== undefined && setSelectValue(tagsInitial); 
        console.log(person.addresses!.filter(p => p.latitude !== 0)[0])
        data !== undefined && setAddreses(person.addresses!.filter(p => p.latitude !== 0)[0]);   
    }, [data]);     

    function filterComunicationChannerls(filter : ICommunicationChannels[], value :string){
        if(value === "Celular Personal"){
            return filter?.find(phone => phone.phoneDescription === value) === undefined 
                    ? ""
                    : filter?.find(phone => phone.phoneDescription === value)?.phoneNumber
        }
        if(value === "Email Personal"){
            return filter?.find(email => email.emailDescription === value) === undefined 
                    ? ""
                    : filter?.find(email => email.emailDescription === value)?.email
        }
    }

    const saveChanges = () => {
        if (!personToEdit.addresses) {
            toast.error("agregue al menos una direccion");
            return
        }
        const ems: IMail[] = [{ emailDescription: "Email Personal", email: personToEdit.email! }];
        const phs: IPhone[] = [{ phoneDescription: "Celular Personal", phoneNumber: personToEdit.phone! }];
        const personToCreate: IPerson = {
          id: personToEdit.id!,
          firstName: personToEdit.firstName!,
          secondName: personToEdit.secondName!,
          lastName: personToEdit.lastName!,
          nationalId: personToEdit.nationalId!,
          birthDate: personToEdit.birthDate!,
          gender: personToEdit.gender!,
          pronounPreference: personToEdit.pronounPreference!,
          addresses: [addreses],
          mails: ems,
          phones: phs,
          tags:personToEdit.tags!
        };
        
        const pwt:IPersonWithTag={
            person:personToCreate as IPerson,
            tags : selectValue.map(tag => {                
                   return {
                        id : tag.value,
                        name : tag.label,
                        description : "",
                        tagCategoryId : tag.valueCategory
                    }                
            })
        }
        console.log(pwt)
        // updatePerson(pwt).then((response: { data: IPerson; } | { error: FetchBaseQueryError | SerializedError; }) => {
        //     if ("data" in response) {
        //         toast.success(`Persona ha sido actualizado exitosamente`);
        //         navigate(`/person`)
        //     }
        //     if ("error" in response) {
        //         toast.error("Error al momento de crear la persona");
        //     }
        // });
        // navigate(`/person`)
        // updatePerson(personToEdit as Partial<IPerson>).then((response: { data: IPerson; } | { error: FetchBaseQueryError | SerializedError; }) => {
        //     if ("data" in response) {
        //         toast.success(`Persona ha sido actualizado existosamente`);
        //         navigate(`/person`)
        //     }
        //     if ("error" in response) {
        //         toast.error("Error al momento de crear la persona");
        //     }
        // });
        // navigate(`/person`)
    }
 
    return (data === undefined ? <LinearProgress color="secondary" /> :
        <MainCard
            title="Editar Persona"
            secondary={
                <CardButton type="back" title="Lista de Personas" link="/person" />
            } >
            <Box
                display="flex"
                justifyContent="center"
                alignItems="center"
            >
                   <Box display="flex"
                    justifyContent="center"
                    alignItems="center"
                    flexDirection={"column"}
                >
                     <Grid container spacing={2}>
                    <Grid item xs={12} md={8}>
                        <PersonalDataSimple person={personToEdit} setPerson={setPersonToEdit} />
                    </Grid>
                    <Grid item xs={12} md={4}>
                        <Tags valueCategories = {valueCategories} selectValue={selectValue} setSelectValue={setSelectValue}/>
                    </Grid>
                    <Grid item xs={12} md={12}>
                        <Addresses person={addreses} setPerson={setAddreses} />
                    </Grid>
                    <Grid item xs={12} md={12} display="flex" justifyContent="center">
                        <LoadingButton
                            loading={isUpdating}
                            text="Guardar todos los cambios"
                            onClick={saveChanges}
                            startIcon={<SaveIcon />}/>
                    </Grid>
                </Grid>        
                </Box>
            </Box>
        </MainCard>);
}

export default Edit;

