using MyServer.Models;
using MyServer.Stores;

namespace MyServer.Actions
{
    public static class RegenerateAllConfigs
    {
        public static void Invoke()
        {
            //Профили
            foreach (Profile profile in ProfileStore.Instance.Profiles)
            {
                DeleteProfileBat.Invoke(profile);
                GenerateProfileBat.Invoke(profile);
            }
            // Домены
            foreach (Domain domain in DomainStore.Instance.Domains)
            {
                // Удаляем конфиги .conf.tpl и .conf
                DeleteConfigsDomain.Invoke(domain);
                // Создаем .conf.tpl
                CreateTemplateConfigDomain.Invoke(domain);
                // Генерируем конфиг для домена
                GenerateConfig.Invoke("userdata/configs/Apache24/vhosts/" + domain.Name + ".conf.tpl");
            }

            // Пересоздаем файл Hosts
            ClearDomainHosts.Invoke();
            SetDomainHosts.Invoke();
        }
    }
}
