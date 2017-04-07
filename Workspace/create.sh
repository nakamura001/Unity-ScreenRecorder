#!/bin/sh

ffmpeg -r 30 -i ../Capture/frame%04d.png -auto-alt-ref 0 -c:v libvpx output.webm