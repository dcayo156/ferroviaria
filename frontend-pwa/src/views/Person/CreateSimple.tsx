import { Box, Button, ButtonGroup } from '@mui/material';
import Grid from '@mui/material/Grid';
import * as React from 'react';
import { useCreatePeopleMutation } from '../../store/services/Person'
import { useNavigate } from 'react-router-dom'
import { IPerson, Gender, IPersonWithTag, IPersonSimple, IMail, IPhone } from '../../store/types/Person'
import MainCard from '../../components/cards/MainCard';
import CardButton from '../../components/cards/CardButton';
import { hasError } from '../../components/Security/ErrorManager';
// import PersonalData from './Forms/PersonalData'
import PersonalDataSimple from './Forms/PersonalDataSimple'
import Addresses from './Forms/Addresses'
import Tags from './Forms/TagsSimple'
import { FetchBaseQueryError } from '@reduxjs/toolkit/dist/query';
import { SerializedError } from '@reduxjs/toolkit';
import { toast } from 'react-toastify';
import SaveIcon from '@mui/icons-material/Save';
import { ITag, selectValuesProps } from '../../store/types/Tag';
import LoadingButton from '../../components/Buttons/LoadingButton'
import { useGetTagCategoryQuery } from '../../store/services/Tag';

const Create = () => {
    const navigate = useNavigate();
    const [
        createPerson,
        { isLoading: isUpdating },
    ] = useCreatePeopleMutation()
    const [personToCreateSimple, setPersonToCreateSimple] = React.useState<IPersonSimple | Partial<IPersonSimple>>({
        id: "",
        firstName: "",
        secondName: "",
        lastName: "",
        nationalId: "",
        birthDate: "2022-06-21T20:14:15.005Z",
        gender: Gender.Personalizado,
        pronounPreference: "Femenino",
        addresses: [{
            street:"",
            city: 0,
            state: 0,
            country: 0,
            zipCode: "",
            latitude: 0,
            longitude: 0,
            description: ""
        }],
        email: "@gmail.com",
        phone: ""
    });

    const { data: categories, error:categorie_error, isLoading:categorie_esLoading } = useGetTagCategoryQuery();
    const [selectValue,setSelectValue]=React.useState<selectValuesProps[]>([]);
    const [valueCategories,setValueCategories]=React.useState<selectValuesProps[]>([]);
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

    const saveChanges = () => {
        if (!personToCreateSimple.addresses) {
            toast.error("agregue al menos una direccion");
            return
        }

        const ems: IMail[] = [{ emailDescription: "Email Personal", email: personToCreateSimple.email! }];
        const phs: IPhone[] = [{ phoneDescription: "Celular Personal", phoneNumber: personToCreateSimple.phone! }];

        const personToCreate: IPerson = {
          id: personToCreateSimple.id!,
          firstName: personToCreateSimple.firstName!,
          secondName: personToCreateSimple.secondName!,
          lastName: personToCreateSimple.lastName!,
          nationalId: personToCreateSimple.nationalId!,
          birthDate: personToCreateSimple.birthDate!,
          gender: personToCreateSimple.gender!,
          pronounPreference: personToCreateSimple.pronounPreference!,
          addresses: personToCreateSimple.addresses,
          mails: ems,
          phones: phs,
          tags:personToCreateSimple.tags!
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
        createPerson(pwt).then((response: { data: string } | { error: FetchBaseQueryError | SerializedError; }) => {
            if(hasError(response,"Error al momento de crear la persona")){
                return;
            }
            if ("data" in response) {
                const fullname = `${personToCreate.firstName} ${personToCreate.secondName} ${personToCreate.lastName}`
                toast.success(`Persona: ${fullname} ha sido creado existosamente`);
                navigate(`/person`)
            }
        });

    }
    const onCancelChanges = () => {
        navigate(`/person`)
    }
    return (
        <MainCard
            title="Registrar Persona"
            secondary={
                <CardButton type="back" title="Lista de Personas" link="/person" />
            } >
            <Box
                display="flex"
                justifyContent="center"
                alignItems="center"
            >
                <Grid container spacing={2}>
                    <Grid item xs={12} md={8}>
                        <PersonalDataSimple person={personToCreateSimple} setPerson={setPersonToCreateSimple} />
                    </Grid>
                    <Grid item xs={12} md={4}>
                       <Tags valueCategories = {valueCategories} selectValue={selectValue} setSelectValue={setSelectValue}/>
                    </Grid>
                    <Grid item xs={12} md={12}>
                        <Addresses person={personToCreateSimple} setPerson={setPersonToCreateSimple} />
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
        </MainCard>);
}

export default Create;