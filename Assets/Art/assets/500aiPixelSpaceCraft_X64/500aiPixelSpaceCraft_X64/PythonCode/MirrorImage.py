import os
from PIL import Image

def mirror_and_split_image(input_folder):
    # 检查输入文件夹路径是否存在
    # Check if the input folder path exists
    if not os.path.exists(input_folder):
        print(f"输入文件夹 {input_folder} 不存在。")
        return
    
    # 创建输出文件夹
    # Create the output folder
    output_folder = input_folder.rstrip('/') + '_Mirror'
    os.makedirs(output_folder, exist_ok=True)
    
    # 遍历输入文件夹中的所有PNG文件
    # Iterate through all PNG files in the input folder
    for filename in os.listdir(input_folder):
        if filename.endswith('.png'):
            input_path = os.path.join(input_folder, filename)
            image = Image.open(input_path)
            width, height = image.size

            # 左半部分
            # Left half
            left_half = image.crop((0, 0, width // 2, height))
            left_mirror = left_half.transpose(Image.FLIP_LEFT_RIGHT)
            left_image = Image.new('RGBA', (width, height))
            left_image.paste(left_half, (0, 0))
            left_image.paste(left_mirror, (width // 2, 0))
            left_output_path = os.path.join(output_folder, f'{os.path.splitext(filename)[0]}_left_mirror.png')
            left_image.save(left_output_path)

            # 右半部分
            # Right half
            right_half = image.crop((width // 2, 0, width, height))
            right_mirror = right_half.transpose(Image.FLIP_LEFT_RIGHT)
            right_image = Image.new('RGBA', (width, height))
            right_image.paste(right_half, (width // 2, 0))
            right_image.paste(right_mirror, (0, 0))
            right_output_path = os.path.join(output_folder, f'{os.path.splitext(filename)[0]}_right_mirror.png')
            right_image.save(right_output_path)

            # Processed file: {filename}
            print(f"已处理文件: {filename}")

if __name__ == "__main__":
    input_folder = r"Your folder"
    mirror_and_split_image(input_folder)


