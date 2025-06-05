from fastapi import FastAPI
from pydantic import BaseModel

from services.llm import get_generation_plan

app = FastAPI()


@app.get("/")
async def root():
    return {"status": "healthy"}

@app.get("/api/heath")
async def heath():
    return {"status": "healthy"}


class PostGenerationPlanDto(BaseModel):
    prompt: str

@app.post("/api/plan/generate")
async def generate_plan(dto: PostGenerationPlanDto):
    return get_generation_plan(dto.prompt)


