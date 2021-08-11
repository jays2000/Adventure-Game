/**
 * Care taker class for Memento design pattern
 * Responsible to restore object state from momento
 * 
 */
using System;
using System.Collections.Generic;

namespace StarterGame
{
    public class CareTaker
    {
        private Stack<Momento> MomentoStack; //holds mementos

        public CareTaker()
        {
            MomentoStack = new Stack<Momento>();
        }

        public void add(Momento momento)
        {
            MomentoStack.Push(momento);
        }

        public Momento Get() // get last saved memento
        { 
            if(MomentoStack.Count != 0)
            {
                return MomentoStack.Pop();
            }
            else
            {
                return null;
            }
        }
    }
}
