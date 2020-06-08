﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoOp19API.DataAccess
{
    class Input
    {
        private DB19Context context;

        public Input(DB19Context cont)
        {
            context = cont;
        }

        /// <summary>
        /// adds an item to the database
        /// </summary>
        /// <typeparam name="T">type of data</typeparam>
        /// <param name="item">input item</param>
        /// <returns>added item with updated ids</returns>
        public async Task<T> Add<T>(T item) where T : class
        {
            var querry = context.Set<T>();
            EntityEntry<T> output;
            try
            {
                output = await querry.AddAsync(item);
            }
            catch (Exception E)
            {
                throw new Exception("Input",
                    new Exception($"New item is invalid: {E.Message}"));
            }
            _ = await context.SaveChangesAsync();
            return output.Entity;
        }
    }
}
