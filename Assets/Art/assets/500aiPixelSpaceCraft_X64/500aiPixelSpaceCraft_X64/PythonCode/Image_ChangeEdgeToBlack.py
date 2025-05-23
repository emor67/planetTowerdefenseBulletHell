import os
from PIL import Image
# 这个代码是用来包边的，会将飞机边缘改为黑色。而且会生成一个_Out文件夹来保存。
# This code is used for outlining, it will change the edges of the airplane to black.And it will generate an '_Out' folder to save.

def process_image(image):
    width, height = image.size
    pixels = image.load()

    for x in range(width):
        for y in range(height):
            r, g, b, a = pixels[x, y]
            if a != 0:  # 透明通道不为0
                # 检查九宫格中的透明像素
                # Check for transparent pixels in the 3x3 grid
                transparent_found = False
                for dx in [-1, 0, 1]:
                    for dy in [-1, 0, 1]:
                        nx, ny = x + dx, y + dy
                        if 0 <= nx < width and 0 <= ny < height:
                            _, _, _, na = pixels[nx, ny]
                            if na == 0:  # 发现透明像素
                                # Found a transparent pixel
                                transparent_found = True
                                break
                    if transparent_found:
                        break

                if transparent_found:
                    pixels[x, y] = (0, 0, 0, 255)

    return image

def process_images_in_folder(input_folder):
    # 检查输入文件夹路径是否存在
    # Check if the input folder path exists
    if not os.path.exists(input_folder):
        print(f"输入文件夹 {input_folder} 不存在。")
        return

    # 创建输出文件夹
    # Create the output folder
    output_folder = input_folder.rstrip('/') + '_Out'
    os.makedirs(output_folder, exist_ok=True)

    # 遍历输入文件夹中的所有PNG文件
    # Iterate through all PNG files in the input folder
    for filename in os.listdir(input_folder):
        if filename.endswith('.png'):
            input_path = os.path.join(input_folder, filename)
            image = Image.open(input_path).convert('RGBA')
            
            # 处理图片
            # Process the image
            processed_image = process_image(image)
            
            # 保存图片到输出文件夹
            # Save the image to the output folder
            output_path = os.path.join(output_folder, filename)
            processed_image.save(output_path)

            print(f"已处理文件: {filename}")

if __name__ == "__main__":
    input_folder = r"Your folder"
    process_images_in_folder(input_folder)
