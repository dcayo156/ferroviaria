import { Box, Button, Grid, IconButton, TextField, Typography } from '@mui/material';
import * as React from 'react';
import GeoCode from './GeoCode'
import { IPersonSimple, IAddress } from '../../../store/types/Person';
import AddIcon from '@mui/icons-material/Add';
import PageviewIcon from '@mui/icons-material/LocationOn';
import DeleteIcon from '@mui/icons-material/Delete';
import FormCard from '../../../components/cards/FormCard'
import { reactHooksModule } from '@reduxjs/toolkit/dist/query/react';
import { group } from 'console';
import { IAddressLocation } from '../../../store/types/Address';
interface AddressesProps {
    person: IAddress 
    setPerson: (value: React.SetStateAction<IAddress>) => void
}

const Addresses: React.FunctionComponent<AddressesProps> = ({ person: address, setPerson: setAddress }) => {
    const reciveLocation = (data: IAddress) => {       
        setAddress(data)            
    }

    const removeAddr = () => {
        setAddress({
            street: "",
            city: 0,
            state: 0,
            country: 0,
            zipCode: "none",
            latitude: 0,
            longitude: 0,
            description: ""
        });
    }

    return (<FormCard 
                icon={<PageviewIcon />} 
                title='Registrar Direccion' 
                action={null} 
                style={undefined}
                key={"registerMap"}>
        <Grid container spacing={2}>
            <Grid item md={8} xs={12}>
                <GeoCode location={{lat: address.latitude, lng : address.longitude}} sendLocation={reciveLocation}></GeoCode>
            </Grid>
            <Grid item md={4} xs={12}>
                <Grid container spacing={1}>
                    <Grid item xs={6} >
                        <Typography variant="h6" component="h2">
                            {`lan:${address.latitude.toFixed(6)}`}
                        </Typography>
                    </Grid>
                    <Grid item xs={6} >
                        <Typography variant="h6" component="h2">
                            {`lng:${address.longitude.toFixed(6)}`}
                        </Typography>
                    </Grid>
                    <Grid item xs={12} >
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            id="street"
                            label="Street"
                            name="street"
                            value={address.street}
                            onChange={({ target: { value } }) => {
                                setAddress({ ...address, street: value })
                            }}
                        />
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            name="description"
                            label="Localidad"
                            type="description"
                            id="description"
                            value={address.description}
                            onChange={({ target: { value } }) => {
                                setAddress({ ...address, description: value })
                            }}
                        />                       
                    </Grid>
                    <Grid item xs={12} >
                    <Box>
                        {address.street}
                            <IconButton onClick={removeAddr} aria-label="delete">
                                <DeleteIcon />
                            </IconButton>
                            <hr />
                    </Box>
                    </Grid>
                </Grid>

            </Grid>
        </Grid>
    </FormCard>
    );
}

export default Addresses;