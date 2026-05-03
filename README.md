# SteamResChanger
A small utility to automatically set your resolution to a defined game resolution when any game is started from Steam, and set it back upon closing.

This is especially useful if you prefer to run your games at a lower resolution for various reasons.

For example, if you play on a TV with HDMI 2.0, which limits you to 1440p@120hz or 4K@60hz, and you want to play games at the higher refresh rate but use your desktop at 4K@60.

## Features
- Detects when any game is started through Steam and automatically applies the game resolution preset, changing it back when the game is closed
- Multiple desktop resolution presets to allow easy switching via tray context menu or via global hotkeys
- HDR can be enabled or disabled per preset, or if the checkbox is left in the default (indeterminate) state then HDR will not be toggled when that preset is applied
- HDR can also be manually toggled through the tray icon or through the bound hotkey

## Note
Non Steam games added to Steam will also be recognized, but this relies on detecting the Steam Overlay rather than the running AppID.
So make sure you don't disable the Steam Overlay for those games (it should be enabled by default), or the detection won't work.

## Screenshot
<img width="1931" height="928" alt="image" src="https://github.com/user-attachments/assets/a183c948-274a-44a1-b1a0-c747370965ab" />
