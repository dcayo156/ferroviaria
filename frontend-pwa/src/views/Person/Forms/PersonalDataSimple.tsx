import * as React from 'react';
import { Gender, IPersonSimple } from '../../../store/types/Person';
import {TextField } from '@mui/material';
import Grid from '@mui/material/Grid';
import PersonIcon from '@mui/icons-material/Person';
import FormCard from '../../../components/cards/FormCard'
interface PersonalDataProps {
    person: IPersonSimple | Partial<IPersonSimple>
    setPerson: (value: React.SetStateAction<IPersonSimple | Partial<IPersonSimple>>) => void
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
                    label="Primer nombre"
                    name="firstName"
                    value={person!.firstName}
                    onChange={({ target: { value } }) => setPerson({ ...person, "firstName": value })}
                    autoFocus
                />
            </Grid>
            <Grid item xs={12} lg={4}>
                <TextField
                    margin="normal"
                    fullWidth
                    name="secondName"
                    label="Segundo nombre"
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
                    label="Apellido"
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
                    label="Fecha de Nacimiento"
                    name="birthDate"
                    type="date"
                    value={person!.birthDate?.split("T")[0]}
                    onChange={({ target: { value } }) => setPerson({ ...person, "birthDate": value })}
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    select
                    label="Género"
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
            <Grid item xs={6}>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    name="email"
                    label="Email"
                    type="email"
                    id="email"
                    value={person!.email}
                    onChange={({ target: { value } }) => setPerson({ ...person, "email": value })}
                />
            </Grid>            
            <Grid item xs={6}>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    name="phone"
                    label="Teléfono"
                    type="phone"
                    id="phone"
                    value={person!.phone}
                    onChange={({ target: { value } }) => setPerson({ ...person, "phone": value })}
                />
            </Grid>
        </Grid>
    </FormCard>
}

export default PersonalData;