import { Box, TextField, Button } from '@mui/material';
import Grid from '@mui/material/Grid'
import * as React from 'react';
import { IPerson, IPhone } from '../../../store/types/Person';
import AddIcon from '@mui/icons-material/Add';
import PhoneIcon from '@mui/icons-material/Phone';
import RemoveIcon from '@mui/icons-material/Delete';
import FormCard from '../../../components/cards/FormCard'

interface PhonesProps {
    person: IPerson | Partial<IPerson>
    setPerson: (value: React.SetStateAction<IPerson | Partial<IPerson>>) => void
}

const Phones: React.FunctionComponent<PhonesProps> = ({ person, setPerson }) => {
    const [phones, setPhones] = React.useState<IPhone[]>(person.phones as IPhone[])
    const addPhones = () => {
        const em: IPhone = { phoneDescription: "Celular Personal", phoneNumber: "" };
        setPhones(phones.concat(em));
    }
    const deletePhones = (index: number) => {
        let e1 = [...phones];
        e1.splice(index, 1)
        setPhones(e1);
    }
    const onChangePhones = (index: number, phone: IPhone) => {
        let e1 = [...phones];
        e1.splice(index, 1, phone)
        setPhones(e1)
    }
    React.useEffect(() => { setPerson({ ...person, "phones": phones }) }, [phones])
    return <FormCard 
                icon={<PhoneIcon />} 
                title='Registar Telefonos' 
                style={undefined}
                action={
                    <Button onClick={addPhones} variant="contained" aria-label="settings">
                        <AddIcon />
                    </Button>
                    } 
                key='PhoneRegister'>
        <Box>
            {
                phones.map((phone, index) => {
                    return <Grid container spacing={2} key={index}>
                        <Grid item xs={12} md={5}>
                            <TextField
                                margin="normal"
                                required
                                fullWidth
                                id="phoneDescription"
                                label="Description"
                                name="phoneDescription"
                                value={phone.phoneDescription}
                                onChange={({ target: { value } }) => {
                                    phone.phoneDescription = value
                                    onChangePhones(index, phone)
                                }}
                                autoFocus
                            />
                        </Grid>
                        <Grid item xs={10} md={5}>
                            <TextField
                                margin="normal"
                                required
                                fullWidth
                                name="phone"
                                label="Phones"
                                type="phone"
                                id="phone"
                                value={phone.phoneNumber}
                                onChange={({ target: { value } }) => {
                                    phone.phoneNumber = value
                                    onChangePhones(index, phone)
                                }}
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <Button sx={{ mt: 2 }} onClick={() => { deletePhones(index) }} variant="contained">
                                <RemoveIcon />
                            </Button>
                        </Grid>
                    </Grid >
                })
            }
        </Box >
    </FormCard>

}

export default Phones;