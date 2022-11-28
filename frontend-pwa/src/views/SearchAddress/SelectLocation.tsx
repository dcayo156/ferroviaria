import * as React from 'react';
import { Button, Grid, TextField, MenuItem, IconButton, Menu } from "@mui/material";
import ArrowDownwardIcon from '@mui/icons-material/ArrowDownward';
interface SelectLocationProps {
    isLoaded:boolean
    setCenter: React.Dispatch<React.SetStateAction<google.maps.LatLng | google.maps.LatLngLiteral | undefined>>
    map:any
    changeCenter: (cent:google.maps.LatLng | google.maps.LatLngLiteral | undefined,loc: string)=>void
}
//@Deprecate
const SelectLocation: React.FunctionComponent<SelectLocationProps> = ({isLoaded,setCenter,map,changeCenter}) => {
    const [localidad, setLocalidad] = React.useState<string>("");
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const [predictions, setPredictions] = React.useState<google.maps.GeocoderResult[] | null>(null);
    const open = Boolean(anchorEl);
    const inputref = React.useRef<HTMLDivElement>(null);
    const handleClose = (index: number, value: string) => {
        if (index !== -1) {
            setLocalidad(value)
            changeCenter(predictions![index].geometry?.location,value)
        }
        setLocalidad(value)
        setAnchorEl(null);
    };
    const onOpenMenu = (event: React.MouseEvent<HTMLButtonElement>) => {
        const d: HTMLElement = inputref.current?.childNodes[1] as HTMLElement;
        setAnchorEl(d);
    }
    const onSelectLocation = (event: React.MouseEvent<HTMLButtonElement>) => {
        if (isLoaded && map != null) {
            const geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': localidad }, function (results, status) {
                if (status == 'OK') {
                    console.log(results);
                    setPredictions(results)

                } else {
                    setPredictions(null)
                }
                const d: HTMLElement = inputref.current?.childNodes[1] as HTMLElement;
                setAnchorEl(d);
            });
        }
    }

    return <>
        <Grid item>
            <TextField
                ref={inputref}
                id="localidad"
                fullWidth
                label="Localidad"
                value={localidad}
                onChange={(e) => { setLocalidad(e.target.value) }}
                helperText="Sea muy descriptivo en la busqueda"
                InputProps={{
                    endAdornment: <IconButton color="primary" aria-label="Buscar" onClick={onOpenMenu}>
                        <ArrowDownwardIcon />
                    </IconButton>
                }}
            >
            </TextField>
            <Menu
                id="basic-menu"
                anchorEl={anchorEl}
                open={open}
                onClose={() => handleClose(-1, localidad)}
                MenuListProps={{
                    'aria-labelledby': 'basic-button',
                }}
            >
                {
                    predictions != null ?
                        predictions.map((prediction, index) => {
                            return <MenuItem key={index} onClick={() => handleClose(index, prediction.formatted_address)}>{prediction.formatted_address}</MenuItem>
                        })
                        :
                        <MenuItem onClick={() => handleClose(-1, localidad)}>no se encontro resultados</MenuItem>
                }
            </Menu>
        </Grid>
        <Grid item>
            <Button onClick={onSelectLocation} variant="contained">Buscar</Button>
        </Grid>
    </>
}

export default SelectLocation;