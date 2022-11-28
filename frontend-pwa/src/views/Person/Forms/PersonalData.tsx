import * as React from 'react';
import { Gender, IPerson } from '../../../store/types/Person';
import Personalizado from '../../../assets/person/Personalizado.png'
import { Avatar, Box, Card, CardContent, CardHeader, TextField } from '@mui/material';
import Grid from '@mui/material/Grid';
import PersonIcon from '@mui/icons-material/Person';
import FormCard from '../../../components/cards/FormCard'
interface PersonalDataProps {
    person: IPerson | Partial<IPerson>
    setPerson: (value: React.SetStateAction<IPerson | Partial<IPerson>>) => void
}
export const getFecha = (date: string) => {
    const today: Date = new Date(date);
    today.setDate(today.getDate() + 1);
    var mm = today.getMonth() + 1;
    var dd = today.getDate();

    return [today.getFullYear(),
    (mm > 9 ? '' : '0') + mm,
    (dd > 9 ? '' : '0') + dd
    ].join('-');
}
const PersonalData: React.FunctionComponent<PersonalDataProps> = ({ person, setPerson }) => {
    return <FormCard 
                icon={<PersonIcon />} 
                style={undefined}
                title='Registrar Datos Personales' 
                key='registerPeople' 
                action={null}>
        <Grid container spacing={2}>
            <Grid item xs={12} lg={4}>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    id="firstName"
                    label="First Name"
                    name="firstName"
                    value={person!.firstName}
                    onChange={({ target: { value } }) => setPerson({ ...person, "firstName": value })}
                    autoFocus
                />
            </Grid>
            <Grid item xs={12} lg={4}>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    name="secondName"
                    label="Second Name"
                    type="secondName"
                    id="secondName"
                    value={person!.secondName}
                    onChange={({ target: { value } }) => setPerson({ ...person, "secondName": value })}
                />
            </Grid>
            <Grid item xs={12} lg={4}>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    id="lastName"
                    label="Last Name"
                    name="lastName"
                    value={person!.lastName}
                    onChange={({ target: { value } }) => setPerson({ ...person, "lastName": value })}
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    id="birthDate"
                    label="BirthDate"
                    name="birthDate"
                    type="date"
                    value={person!.birthDate?.split("T")[0]}
                    onChange={({ target: { value } }) => setPerson({ ...person, "birthDate": value })}
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    select
                    label="Gender"
                    sx={{ width: "100%", mt: 2 }}
                    value={person.gender}
                    onChange={({ target: { value } }) => setPerson({ ...person, "pronounPreference": value, "gender": value as Gender })}
                    SelectProps={{
                        native: true,
                    }}
                >

                    <option key={Gender.Personalizado} value={Gender.Personalizado}>
                        {Gender.Personalizado}
                    </option>
                    <option key={Gender.Masculino} value={Gender.Masculino}>
                        {Gender.Masculino}
                    </option>
                    <option key={Gender.Femenino} value={Gender.Femenino}>
                        {Gender.Femenino}
                    </option>
                </TextField>
            </Grid>
        </Grid>
    </FormCard>
}

export default PersonalData;