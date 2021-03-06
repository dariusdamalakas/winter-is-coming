﻿namespace WinterIsComing.Server
{
    public interface IGameCommandDispatcher
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Response that is send to the user</returns>
        void Dispatch(IGameCommand command);
    }
}