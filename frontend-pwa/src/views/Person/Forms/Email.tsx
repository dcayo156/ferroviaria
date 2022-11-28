import { Box, TextField, Button } from '@mui/material';
import Grid from '@mui/material/Grid'
import * as React from 'react';
import { IPerson, IMail } from '../../../store/types/Person';
import AddIcon from '@mui/icons-material/Add';
import EmailIcon from '@mui/icons-material/Email';
import RemoveIcon from '@mui/icons-material/Delete';
import FormCard from '../../../components/cards/FormCard'

interface EmailProps {
    person: IPerson | Partial<IPerson>
    setPerson: (value: React.SetStateAction<IPerson | Partial<IPerson>>) => void
}

const Email: React.FunctionComponent<EmailProps> = ({ person, setPerson }) => {
    const [emails, setEmails] = React.useState<IMail[]>(person.mails as IMail[])
    const addEmail = () => {
        const em: IMail = { emailDescription: "Email Personal", email: "@gmail.com" };
        setEmails(emails.concat(em));
    }
    const deleteEmail = (index: number) => {
        let e1 = [...emails];
        e1.splice(index, 1)
        setEmails(e1);
    }
    const onChangeEmail = (index: number, email: IMail) => {
        let e1 = [...emails];
        e1.splice(index, 1, email)
        setEmails(e1)
    }
    React.useEffect(() => { setPerson({ ...person, "mails": emails }) }, [emails])
    return <FormCard
        style={undefined}
        icon={<EmailIcon />}
        title='Registrar Emails'
        action={<Button
            onClick={addEmail}
            variant="contained"
            aria-label="settings">
            <AddIcon />
        </Button>}
        key="EmialRegister">
        <Box>
            {
                emails.map((email, index) => {
                    return <Grid container spacing={2} key={index}>
                        <Grid item xs={12} md={5}>
                            <TextField
                                margin="normal"
                                required
                                fullWidth
                                id="emailDescription"
                                label="Description"
                                name="emailDescription"
                                value={email.emailDescription}
                                onChange={({ target: { value } }) => {
                                    email.emailDescription = value
                                    onChangeEmail(index, email)
                                }}
                                autoFocus
                            />
                        </Grid>
                        <Grid item xs={10} md={5}>
                            <TextField
                                margin="normal"
                                required
                                fullWidth
                                name="email"
                                label="Email"
                                type="email"
                                id="email"
                                value={email.email}
                                onChange={({ target: { value } }) => {
                                    email.email = value
                                    onChangeEmail(index, email)
                                }}
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <Button sx={{ mt: 2 }} onClick={() => { deleteEmail(index) }} variant="contained">
                                <RemoveIcon />
                            </Button>
                        </Grid>
                    </Grid >
                })
            }
        </Box >
    </FormCard>

}

export default Email;