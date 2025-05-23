import os
from PIL import Image
#这个代码会直接替换原图，请在运行前务必保存备份！
# This code will directly replace the original image, please make sure to save a backup before running!

def is_transparent(pixel):
    # 检查像素的透明度通道是否为0
    # Check if the alpha channel of the pixel is 0
    return pixel[3] == 0

def modify_image(image_path):
    img = Image.open(image_path).convert("RGBA")
    pixels = img.load()  # 加载图像的像素数据
    # Load pixel data of the image

    width, height = img.size

    # 创建一个新的图像对象来缓存修改后的结果
    # Create a new image object to store the modified result
    new_img = Image.new("RGBA", (width, height))
    new_pixels = new_img.load()

    for y in range(height):
        for x in range(width):
            pixel_A = pixels[x, y]
            # 如果当前像素是透明的
            # If the current pixel is transparent
            if is_transparent(pixel_A):
                # 查找九宫格
                # Look for the 3x3 grid
                non_transparent_found = False
                for dy in range(-1, 2):  # -1, 0, 1
                    for dx in range(-1, 2):  # -1, 0, 1
                        if 0 <= x + dx < width and 0 <= y + dy < height:
                            neighbor_pixel = pixels[x + dx, y + dy]
                            if not is_transparent(neighbor_pixel):
                                non_transparent_found = True
                                break
                    if non_transparent_found:
                        break
                
                # 如果在九宫格中找到非透明像素，则将当前透明像素颜色设为白色
                # If a non-transparent pixel is found in the 3x3 grid, set the current transparent pixel to white
                if non_transparent_found:
                    new_pixels[x, y] = (255, 255, 255, 255)  # 修改为白色
                else:
                    new_pixels[x, y] = pixel_A  # 保留原来的透明像素
            else:
                new_pixels[x, y] = pixel_A  # 保留非透明像素

    # 保存修改后的图像
    # Save the modified image
    new_img.save(image_path)

def traverse_directory(directory):
    for root, _, files in os.walk(directory):
        for file in files:
            if file.lower().endswith('.png'):
                image_path = os.path.join(root, file)
                print(f'Modifying: {image_path}')
                modify_image(image_path)

# 修改为你的目标文件夹路径
# Replace with your target directory path
target_directory = r'Your folder'
traverse_directory(target_directory)
