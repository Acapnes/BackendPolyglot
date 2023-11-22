from fastapi import Request, FastAPI


async def my_middleware(request: Request, call_next):
    # Middleware işlemleri burada yapılır
    response = await call_next(request)
    # Middleware işlemleri burada yapılır
    return response
