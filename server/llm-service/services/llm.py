import json

from google import genai
from google.genai import types
from PIL import Image
from io import BytesIO
from pydantic import BaseModel
import os
import base64

client = genai.Client(api_key=os.getenv("GEMINI_KEY"))



class Command(BaseModel):
    name: str
    arguments: list[str]


class GenerationPLan(BaseModel):
    commands: list[Command]


def get_generation_plan(prompt: str) -> str:
    contents = [
        types.Content(
            role='model',
            parts=[
                types.Part(
                    text=(
                        "You are an assistant for fantasy map generation. "
                        "You will be given a user prompt and will return a JSON object with a list of map generation commands. "
                        "The canvas size is fixed at 512x512 cells. By default, all cells are water. "
                        "There are two cell types: 'land' and 'water'.\n\n"

                        "Your response must consist only of a JSON array of commands. Each command modifies the canvas.\n\n"

                        "Available commands:\n"

                        "- spawn(cell_type, radius, roughness, x, y): creates a feature of the specified type at the given location.\n"
                        "  Parameters:\n"
                        "    - cell_type: 'land' or 'water'\n"
                        "    - radius: size of the feature\n"
                        "    - roughness: 1 to 7 (1 = smooth circle, 7 = jagged irregular shape)\n"
                        "    - x: X-coordinate (0–511)\n"
                        "    - y: Y-coordinate (0–511)\n\n"

                        "- spawn_mountain(height, radius, x, y): creates a mountain on existing land.\n"
                        "  Parameters:\n"
                        "    - height: elevation value between 0 and 1 (typically around 0.5 or less for natural shapes)\n"
                        "    - radius: maximum 160 pixels (smaller radius = steeper mountain)\n"
                        "    - x: X-coordinate (0–511)\n"
                        "    - y: Y-coordinate (0–511)\n\n"

                        "You may use multiple 'spawn' and 'spawn_mountain' commands to form continents, islands, lakes, and mountain ranges. "
                        "Interpret user prompts creatively but remain grounded in map logic. "
                        "Ensure results are spatially consistent and visually plausible for a fantasy world.\n\n"

                        "If the user prompt does not specify details like size, roughness, or coordinates, feel free to use your imagination. "
                        "Prioritize capturing the theme of the request — even if it means improvising a bit. "
                        "Mountains should only be spawned on land regions."
                        "The golden ratio for mountains is height : radius = 0.5 : 160. When improvising, try to keep the ratio close to this or steeper (e.g., height 0.25 for radius 80, height 0.375 for radius 120, etc.)."
                    )
                )
            ]
        ),
        types.Content(
            role='user',
            parts=[
                types.Part(
                    text=prompt
                )
            ]
        ),
    ]

    response = client.models.generate_content(
        model="gemini-2.0-flash",
        config={
            "response_mime_type": "application/json",
            "response_schema": list[GenerationPLan],
        },
        contents=contents,
    )

    return json.loads(response.candidates[0].content.parts[0].text)
