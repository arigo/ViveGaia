Star renderer for HTC Vive for the Gaia project
-----------------------------------------------

A copy of the Gaia Release DR1 star positions and apparent magnitude.

https://gaia.esac.esa.int/documentation/GDR1/datamodel/

The script in 'Assets/ProcessedData/' extracts four numbers per
star listed in http://cdn.gea.esac.esa.int/Gaia/tgas_source/csv/ .

It is meant to be rendered using HTC Vive, but probably works with
other VR systems too.

After loading the Unity project, you need to manually import the
SteamVR basic library to 'Assets/Lib/SteamVR'.