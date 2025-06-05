echo "{\"Logging\":{\"LogLevel\":{\"Default\":\"${LOG_LEVEL:-Information}\",\"Microsoft.AspNetCore\":\"${MS_LOG_LEVEL:-Warning}\"}},\"ApiKey\":\"${API_KEY}\",\"LlmService\":{\"url\":\"${LLM_URL}\"},\"MapGenService\":{\"url\":\"${MAPGEN_URL}\"}}" > appsettings.json

dotnet Api.dll