# How to Use These Scripts

## 1. MirrorImage.py

This script mirrors images horizontally and creates an output folder named "YourPath_Mirror" appended to the original folder path.

### Instructions:

1. Place the images you want to process into a folder.
2. Replace "YourPath" in the script with the path to your folder.
3. Run the script to generate a folder named "YourPath_Mirror" containing mirrored copies of the processed images.

---

## 2. Image_ChangeEdgeToBlack.py

This script changes the color of pixels that touch transparent areas to black. Modified files are saved to an "YourPath_Out" folder without affecting the original images.

### Instructions:

1. Place PNG images you want to process into a folder.
2. Replace "YourPath" in the script with the path to your folder.
3. Run the script to generate a folder named "YourPath_Out" containing the modified images with edges changed to black.

---

## 3. AddWhiteEdge_Border.py

This script adds a white border around images that have already been processed with black edges (from Image_ChangeEdgeToBlack.py). It extends the border outside the image.

**Note: This script will overwrite the original images! Make sure you have backups before running.**

### Instructions:

1. Ensure you have already run and saved images processed with Image_ChangeEdgeToBlack.py.
2. Place the processed images into a folder.
3. Replace "YourPath" in the script with the path to your folder.
4. Run the script to generate a folder named "YourPath" containing copies of the processed images with an additional white border.

