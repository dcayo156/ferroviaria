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
    person: IPersonSimple | Partial<IPersonSimple>
    setPerson: (value: React.SetStateAction<IPersonSimple | Partial<IPersonSimple>>) => void
}

const Addresses: React.FunctionComponent<AddressesProps> = ({ person, setPerson }) => {
    const [addressArray, setAddressArray] = React.useState<IAddress[]>(person.addresses as IAddress[])
    const [address, setAddress] = React.useState<IAddress>({
        street: "",
        city: 0,
        state: 0,
        country: 0,   
        zipCode: "none",
        latitude: 0,
        longitude: 0,
        description: ""
    })
    React.useEffect(() => { 
        setAddress(person?.addresses![0]); 
    }, [])
    const reciveLocation = (data: IAddress) => {
        setAddress(data)
        //console.log("1 => setAddress", data)
    }
    const addAddress = () => {
        setAddressArray([...addressArray].concat(address))
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
    const removeAddr = (index: number) => {
        let e1 = [...addressArray];
        e1.splice(index, 1)
        setAddressArray(e1);
    }
    
    React.useEffect(() => { 
        setPerson((p) => ({...p, "addresses": addressArray}))
    }, [addressArray])
    return (<FormCard 
                icon={<PageviewIcon />} 
                title='Registrar Direccion' 
                action={null} 
                style={undefined}
                key={"registerMap"}>
        <Grid container spacing={2}>
            <Grid item md={8} xs={12}> 
            <GeoCode location={{lat:  address.latitude, lng : address.longitude}} sendLocation={reciveLocation}></GeoCode>
                {/* <GeoCode location={{lat: person?.addresses?.filter(p => p.latitude !== 0)[0].latitude!,
                                    lng : person?.addresses?.filter(p => p.latitude !== 0)[0].longitude!}} 
                        sendLocation={reciveLocation}></GeoCode> */}
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
                        <Button disabled={address.street === ""} onClick={addAddress} variant="contained">
                            <AddIcon />
                        </Button>
                    </Grid>
                    <Grid item xs={12} >
                        {addressArray.map((addr, index) => {
                            return <div key={index}>
                                <Box>
                                    {addr.street}
                                    <IconButton onClick={() => { removeAddr(index) }} aria-label="delete">
                                        <DeleteIcon />
                                    </IconButton>
                                </Box>
                                <hr />
                            </div>
                        })}
                    </Grid>
                </Grid>

            </Grid>
        </Grid>
    </FormCard>
    );
}

export default Addresses;