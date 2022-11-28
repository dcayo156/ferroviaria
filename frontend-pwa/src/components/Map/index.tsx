import * as React from 'react';
import { GoogleMap, GoogleMapProps, Marker, useJsApiLoader } from "@react-google-maps/api";
import PlaceSearch from './PlaceSearch'
import IconCentral from "../../assets/img/central.png"
interface MapProps {
    onClick:((e: google.maps.MapMouseEvent) => void) | undefined
    children: React.ReactNode[] | React.ReactNode
    getIsLoaded:(isLoaded:boolean) => void
    getMap:(map:any) => void | undefined
    onDragEnd:(() => void | Promise<void>) | undefined
    onZoomChanged:(() => void | Promise<void>) | undefined
    actionBeforeFind:(center:google.maps.LatLng )=>void | undefined
    centerTo:google.maps.LatLng | google.maps.LatLngLiteral | undefined
}
const zoom = 15;
const mapId:string =  process.env.REACT_APP_GOOGLE_MAPS_ID as string; 
const libraries: ("places" | "drawing" | "geometry" | "localContext" | "visualization")[] = ['places'];
const Map: React.FunctionComponent<MapProps|Partial<MapProps>> = ({ 
    children,
    getIsLoaded,
    onClick=undefined,
    onDragEnd=undefined,
    onZoomChanged=undefined,
    getMap=undefined,
    actionBeforeFind=undefined,
    centerTo=undefined }) => {
    const [map, setMap] = React.useState(null);
    const onLoad = React.useCallback(function callback(m: any) {
        setMap(m);
        getMap && getMap(m);
    }, []);
    const onUnmount = React.useCallback(function callback(map: any) {
        setMap(null);
    }, []);
    const { isLoaded } = useJsApiLoader({
        googleMapsApiKey: process.env.REACT_APP_GOOGLE_MAPS_API_KEY as string,
        libraries: libraries,
    });
    const [center,setCenter]=React.useState<google.maps.LatLng | google.maps.LatLngLiteral | undefined>({
        lat: -34.63714351,
        lng: -58.70291054
    })
    const ref = React.useRef<HTMLDivElement>(null);
    const [containerStyle, setContaiderStyle] = React.useState({
        width: "100%",
        height: "100vh"
    })

    React.useEffect(() => {
        if (ref.current) {
            const element: HTMLDivElement | null = ref.current;
            setContaiderStyle({
                width: `${element?.clientWidth}px`,
                height: `${element?.clientHeight}px`
            })
        }
    }, [ref])
    React.useEffect(()=>{
        getIsLoaded && getIsLoaded(isLoaded);
    },[isLoaded])
    React.useEffect(()=>{
        centerTo && setCenter(centerTo);
    },[centerTo])
    const onChangeCenter=(centro:google.maps.LatLng | google.maps.LatLngLiteral | undefined,text:string)=>{
        setCenter(centro);
        setShowCenter(true);
        actionBeforeFind && actionBeforeFind(centro as google.maps.LatLng);
        setTimeout(()=>{
            setShowCenter(false);
        },3000)
        
    }
    const [showCenter,setShowCenter]=React.useState<boolean>(false);
    const onLoadMarker=(marker:any)=>{
        
    }
    return (
        <div style={{ height: "90vh", position:"relative" }} ref={ref}>
            <PlaceSearch changeCenter={onChangeCenter} isLoaded={isLoaded} map={map} key="PlaceSearch"/>
            {
                isLoaded
                    ?
                    <GoogleMap
                        options={{mapId:mapId}}
                        mapContainerStyle={containerStyle}
                        center={center}
                        zoom={zoom}
                        onLoad={onLoad}
                        onUnmount={onUnmount}
                        onClick={onClick}
                        onDragEnd={onDragEnd}
                        onZoomChanged={onZoomChanged}
                    >
                        {
                            showCenter===true && <Marker onLoad={onLoadMarker}
                            position={center as google.maps.LatLng} icon={{
                                url: IconCentral,
                                anchor: new window.google.maps.Point(25, 23),
                                scaledSize: new window.google.maps.Size(47, 47)
                              }} />
                        }
                        {children}
                    </GoogleMap>
                    :
                    <></>}
        </div>);
}

export default React.memo(Map);